using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Reflection;
using System.Linq;

namespace cpp
{
    public class SetupEditor : Editor
    {

        [MenuItem("CustomPostProcessing/AutoSetup RendererFeatures")]
        public static void Setup()
        {
            SetupRenderFeature();
        }

        private static void SetupRenderFeature()
        {
            var urpAssetType = (GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset).GetType();
            FieldInfo rdfi = urpAssetType.GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
            var rendererData = (rdfi.GetValue(GraphicsSettings.currentRenderPipeline) as ScriptableRendererData[])[0];
            var rendererFeatures = rendererData.rendererFeatures;
            FieldInfo rmfi = rendererData.GetType().GetField("m_RendererFeatureMap", BindingFlags.Instance | BindingFlags.NonPublic);
            var rendererFeatureMap = rmfi.GetValue(rendererData) as List<long>;

            SetupCustomPostprocessing<BorderNoiseFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<CrossHatchingFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<DisplacementNoiseFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<FrameCacheFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<GlitchFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<GlitchFreezeFrameFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<InvertColorFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<NoiseColorFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<PixelateFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<PixelateNoiseFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<PsycedelicFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<RetroGameFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<SimpleNoiseFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<UVMirrorFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<VideoFeedbackFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<VoronoiFeature>(rendererData, rendererFeatures, rendererFeatureMap);
            SetupCustomPostprocessing<ZoomBlurFeature>(rendererData, rendererFeatures, rendererFeatureMap);

            rendererData.SetDirty();
            AssetDatabase.SaveAssets();
        }

        private static void SetupCustomPostprocessing<T>(ScriptableRendererData rendererData, List<ScriptableRendererFeature> rendererFeatures, List<long> rendererFeatureMap) where T : CustomPostProcessingFeature
        {
            if (rendererFeatures.Any(x => x.GetType() == typeof(T))) { return; }

            var borderNoiseFeature = CreateInstance<T>();
            borderNoiseFeature.name = typeof(T).ToString().Split('.')[1];
            rendererFeatures.Add(borderNoiseFeature);
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(borderNoiseFeature, out string guid, out long localid);
            rendererFeatureMap.Add(localid);
            AssetDatabase.AddObjectToAsset(borderNoiseFeature, rendererData);
        }

    }
}

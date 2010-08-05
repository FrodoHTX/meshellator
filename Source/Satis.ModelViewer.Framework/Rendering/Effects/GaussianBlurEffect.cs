using Nexus;
using SlimDX;
using SlimDX.Direct3D9;

namespace Satis.ModelViewer.Framework.Rendering.Effects
{
	public class GaussianBlurEffect : EffectWrapperBase
	{
		public Vector2D Scale
		{
			get { return GetValue<Vector2D>("Scale"); }
			set { SetValue("Scale", value); }
		}

		public BaseTexture Texture
		{
			get { return GetTexture("Texture"); }
			set { SetTexture("Texture", value); }
		}

		public GaussianBlurEffect(Device device)
			: base(EffectUtility.FromResource(device, "Rendering/Effects/GaussianBlurEffect.fx"))
		{
		}
	}
}
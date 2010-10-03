using System.Collections.Generic;
using Nexus;
using Rasterizr;
using Rasterizr.PipelineStages.ShaderStages.Core;

namespace Meshellator.Integration.Rasterizr
{
	public static class ModelLoader
	{
		public static Model FromScene(RasterizrDevice device, Scene scene)
		{
			// Sort scene meshes by transparency.
			var sortedSceneMeshes = scene.Meshes;
			sortedSceneMeshes.Sort((l, r) => r.Material.Transparency.CompareTo(l.Material.Transparency));

			List<ModelMesh> modelMeshes = new List<ModelMesh>();
			foreach (Mesh mesh in sortedSceneMeshes)
			{
				ModelMesh modelMesh = new ModelMesh(device);
				modelMeshes.Add(modelMesh);

				modelMesh.Indices = mesh.Indices;

				modelMesh.Vertices = new List<VertexPositionNormalTexture>();
				for (int i = 0; i < mesh.Positions.Count; ++i)
				{
					Point2D texCoord = (mesh.TextureCoordinates.Count > i)
					                   	? mesh.TextureCoordinates[i].Xy
					                   	: Point2D.Zero;
					modelMesh.Vertices.Add(new VertexPositionNormalTexture(
						mesh.Positions[i], mesh.Normals[i], texCoord));
				}

				BasicEffect effect = new BasicEffect(device, VertexPositionNormalTexture.InputLayout);

				if (!string.IsNullOrEmpty(mesh.Material.DiffuseTextureName))
				{
					effect.Texture = new Texture2D(mesh.Material.DiffuseTextureName);
					effect.TextureEnabled = true;
				}

				effect.DiffuseColor = mesh.Material.DiffuseColor;
				effect.SpecularColor = mesh.Material.SpecularColor;
				effect.SpecularPower = mesh.Material.Shininess;
				effect.Alpha = mesh.Material.Transparency;

				modelMesh.Effect = effect;
			}

			Model model = new Model(modelMeshes);
			return model;
		}
	}
}
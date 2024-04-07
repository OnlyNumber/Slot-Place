using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class UIParticleSystem : MaskableGraphic
{
    [SerializeField] private ParticleSystemRenderer _particleSystemRenderer;
    [SerializeField] private Camera _backCamera;

    [SerializeField] private Texture _texture;

    public override Texture mainTexture => _texture ?? base.mainTexture;

    private void Update()
    {
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(Mesh mesh)
    {
        mesh.Clear();

        if(_particleSystemRenderer != null && _backCamera != null)
        {
            _particleSystemRenderer.BakeMesh(mesh, _backCamera);
        }
    }
}

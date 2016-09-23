using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ReplacementShaderEffect : MonoBehaviour {

    public Shader ReplacementShader;
    public bool global = false;
    public Color NSOutline;
    public float Sharpness = 2.0f;

    void OnValidate() {
        Shader.SetGlobalFloat("_AM_Sharpness", Sharpness);
        if (global) {
            Shader.SetGlobalColor("_AM_NSOutline", NSOutline);
        }
    }

	void OnEnable () {
        if (ReplacementShader != null) {
            GetComponent<Camera>().SetReplacementShader(ReplacementShader, "RenderType");
        }
	}

    void OnDisable () {
        GetComponent<Camera>().ResetReplacementShader();

    }
}

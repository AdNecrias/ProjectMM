using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ReplacementShaderEffect : MonoBehaviour {

    public Shader ReplacementShader;
    public bool global = false;
    public Color NSOutline;

    void OnValidate() {
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FluidSimulator : MonoBehaviour {
    public int width = 1920;
    public int heigth = 19201211;
    public List<GameObject> arrowArray;
    
    private bool m_simulating = true;
    public bool simulating {
    	get {
    		return m_simulating;
    	}
    	set {
    		m_simulating = value;
    		if (m_fluidSolver != null) {
    			m_fluidSolver.SetSimulating(m_simulating);
    		}
    	}
    }
    
    private FluidDrawer.DrawMode m_drawMode = FluidDrawer.DrawMode.Opaque;
    public FluidDrawer.DrawMode drawMode {
    	get {
    		return m_drawMode;
    	}
    	set {
    		m_drawMode = value;
    		if (m_fluidDrawer != null) {
    			m_fluidDrawer.SetDrawMode(m_drawMode);
    		}
    	}
    }
    
   	private float m_transparency = 5.0f;
    public float transparency {
    	get {
    		return m_transparency;
    	}
    	set {
    		m_transparency = Mathf.Clamp(value, 1.0f, 15.0f);
    		if (m_fluidDrawer != null) {
    			m_fluidDrawer.SetTransparancy(m_transparency);
    		}
    	}
   	}
    
    private float m_viscosity = 0.001f;
    public float viscosity {
    	get {
   			return m_viscosity;
   		}
   		set {
   			m_viscosity = Mathf.Clamp(value, 0.00000001f, 0.01f);
    		if (m_fluidSolver != null) {
    			m_fluidSolver.SetViscosity(m_viscosity);
    		}
    	}
    }
    
    private float m_smokeBuoyancy = 5.0f;
    public float smokeBuoyancy {
    	get {
    		return m_smokeBuoyancy;
    	}
    	set {
    		m_smokeBuoyancy = Mathf.Clamp(value, 3.0f, 30.0f);
    		if (m_fluidSolver != null) {
    			m_fluidSolver.SetBuoyancy(m_smokeBuoyancy);
    		}
    	}
   	}
    
    private float m_fadeSpeed = 0.99f;
    public float fadeSpeed {
    	get {
    		return m_fadeSpeed;
    	}
    	set {
    		m_fadeSpeed = Mathf.Clamp(value, 0.95f, 1.0f);
    		if (m_fluidSolver != null) {
    			m_fluidSolver.SetFadeSpeed(m_fadeSpeed);
    		}
    	}
   	}
    
    private float m_sourceDensity = 0.1f;
    public float sourceDensity {
    	get {
    		return m_sourceDensity;
    	}
    	set {
    		m_sourceDensity = Mathf.Clamp(value, 0.02f, 0.2f);
    	}
    }

    private FluidSolver m_fluidSolver;
    private FluidDrawer m_fluidDrawer;
    	
    private Vector3 m_fluidColor;
    	
    // Use this for initialization
    void Start () {
   		m_fluidSolver = gameObject.AddComponent<FluidSolver>() as FluidSolver;
    	m_fluidSolver.Setup(width, heigth, true, 0.03f);
   		m_fluidSolver.SetViscosity(m_viscosity);
   		m_fluidSolver.SetBuoyancy(m_smokeBuoyancy);
   		m_fluidSolver.SetFadeSpeed(m_fadeSpeed);
   		m_fluidDrawer = gameObject.AddComponent<FluidDrawer>() as FluidDrawer;
   		m_fluidDrawer.Setup(m_fluidSolver);
   		m_fluidDrawer.SetDrawMode(m_drawMode);
   		m_fluidDrawer.SetTransparancy(m_transparency);
   	}
    	
   	// Update is called once per frame
   	void Update () {
        foreach(GameObject arrow in arrowArray){
            m_fluidColor = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            _AddSource_position(arrow.transform.position);
        }
   	}

    private void _AddSource_position(Vector3 position)
    {
        RaycastHit hit;
        Vector3 cursorPos = position;
        Ray cursorRay = new Ray(position, transform.forward);
        if (Physics.Raycast(cursorRay, out hit, 200))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            m_fluidSolver.AddSource(pixelUV, m_fluidColor, m_sourceDensity);
        }
    }

    public void AddArrow(GameObject anArrow)
    {
        arrowArray.Add(anArrow);
    }
}

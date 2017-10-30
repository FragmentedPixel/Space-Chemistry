using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPool : MonoBehaviour {

    //--------------------------------
    //IN CASE OF EXTREME CONFUSION----
    //CHECK: https://gamedevelopment.tutsplus.com/tutorials/creating-dynamic-2d-water-effects-in-unity--gamedev-14143
    //--------------------------------

    //our renderer that'll make the top of the liquid visible
    private LineRenderer Body;

    //physics arrays
    private float[] xpositions;
    private float[] ypositions;
    private float[] velocities;
    private float[] accelerations;

    //mass of the nodes
    public float mass=1f;

    //mesh and colliders
    private GameObject[] meshobjects;
    private GameObject[] colliders;
    private Mesh[] meshes;

    //particle system;
    public GameObject splash;

    //material for the top of the liquid
    public Material mat;

    //gameobject for the mesh;
    public GameObject watermesh;

    //constants
    private const float springconstant = 0.02f;
    private const float damping = 0.04f;
    private const float spread = 0.05f;
    private const float z = -1f;

    //liquid properties
    private float baseheight;
    private float left;
    private float bottom;




	// Use this for initialization
	void Start () {
        SpawnWater(-5, 10, 0, -3);
	}

    public void Splash(float xpos, float velocity)
    {
        
        //If the position is within the bounds of the water:
        if (xpos >= xpositions[0] && xpos <= xpositions[xpositions.Length - 1])
        {
            //Offset the x position to be the distance from the left side
            xpos -= xpositions[0];

            //Find which spring we're touching
            int index = Mathf.RoundToInt((xpositions.Length - 1) * (xpos / (xpositions[xpositions.Length - 1] - xpositions[0])));

            //Add the velocity of the falling object to the spring
            velocities[index] += velocity;

            //Set the lifetime of the particle system.
            float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;

            //Set the splash to be between two values in Shuriken by setting it twice.
            splash.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startLifetime = lifetime;

            //Set the correct position of the particle system.
            Vector3 position = new Vector3(xpositions[index], ypositions[index] - 0.35f, 5);

            //This line aims the splash towards the middle. Only use for small bodies of water:
            Quaternion rotation = Quaternion.LookRotation(new Vector3(xpositions[Mathf.FloorToInt(xpositions.Length / 2)], baseheight + 8, 5) - position);

            //Create the splash and tell it to destroy itself.
            GameObject splish = Instantiate(splash, position, rotation) as GameObject;
            Destroy(splish, lifetime + 0.3f);
        }
    }
	
    public void SpawnWater(float Left, float Width, float Top, float Bottom)
    {
       //for floating add a box collider

        //calculate no of edges and nodes we have
        int edgecount = Mathf.RoundToInt(Width) * 5; //five per unit width to give smoothness and lower the performance impact
        int nodecount = edgecount + 1;

        //Add our line renderer and set it up;
        Body = gameObject.AddComponent<LineRenderer>();
       // Body.useWorldSpace = false;
        Body.material = mat;
        Body.material.renderQueue = 1000;
        //Body.SetVertexCount(nodecount);
        Body.positionCount = nodecount;
       // Body.SetWidth(0.1f, 0.1f);
        Body.startWidth = 0.1f;
        Body.endWidth = 0.1f;

        //Declare our physics arrays
        xpositions = new float[nodecount];
        ypositions = new float[nodecount];
        velocities = new float[nodecount];
        accelerations = new float[nodecount];

        //Declare our mesh arrays
        meshobjects = new GameObject[edgecount];
        meshes = new Mesh[edgecount];
        colliders = new GameObject[edgecount];

        //Set the variables
        baseheight = Top;
        bottom = Bottom;
        left = Left;
        

        //for each node, set the line renderer and physics arrays
        for (int i = 0; i < nodecount; i++)
        {
            ypositions[i] = this.transform.localPosition.y + Top;
            xpositions[i] = this.transform.localPosition.x +(Left + Width * i / edgecount);
            Body.SetPosition(i, new Vector3(xpositions[i], Top, z));
            accelerations[i] = 0;
            velocities[i] = 0;
        }


        //set the meshes

        for (int i = 0; i < edgecount; i++)
        {
            //make the mesh
            meshes[i] = new Mesh();
            
            //mesh corners
            Vector3[] Vertices = new Vector3[4]; //each mesh has 4 corners
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z); //top part is variable where the waves are
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z); //top part is variable where the waves are
            Vertices[2] = new Vector3(xpositions[i], bottom, z); //mesh bottom is flat
            Vertices[3] = new Vector3(xpositions[i+1], bottom, z); //mesh bottom is flat

            //UVs of the mesh's texture
            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            //Set where the triangles should be.
            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            //Add all this data to the mesh.
            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            //Create a holder for the mesh, set it to be the manager's child
            meshobjects[i] = Instantiate(watermesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshobjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshobjects[i].transform.parent = transform;

            //Create our colliders, set them be our child.
            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider2D>();
            colliders[i].transform.parent = transform;

            //set the pos and scale to the correct dimens
            colliders[i].transform.position = new Vector3(Left + Width * (i + 0.5f) / edgecount, Top - 0.5f, 0);
            colliders[i].transform.localScale = new Vector3(Width / edgecount, 1, 1);

            //add WaterDetector and make sure they're triggers
            colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            colliders[i].AddComponent<LiquidDetector>();
            
        }
    }

    void UpdateMeshes()
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
            Vertices[2] = new Vector3(xpositions[i], bottom, z);
            Vertices[3] = new Vector3(xpositions[i + 1], bottom, z);

            meshes[i].vertices = Vertices;
        }
    }

    void FixedUpdate()
    {
        //euler for the physics
        for (int i = 0; i < xpositions.Length; i++) {
            float force = springconstant * (ypositions[i] - baseheight) + velocities[i] * damping;
            accelerations[i] = -force/mass;
            ypositions[i] += velocities[i];
            velocities[i] += accelerations[i];
            Body.SetPosition(i, new Vector3(xpositions[i], ypositions[i], z));
        }

        //Wave creation
        //we store the difference in heights
        float[] leftDeltas = new float[xpositions.Length];
        float[] rightDeltas = new float[xpositions.Length];

        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < xpositions.Length; i++)
            {
                //check height nearby nodes, adjust velocity accordingly, record the height difference
                if (i > 0)
                {
                    leftDeltas[i] = spread * (ypositions[i] - ypositions[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < xpositions.Length - 1)
                {
                    rightDeltas[i] = spread * (ypositions[i] - ypositions[i + 1]);
                    velocities[i + 1] += rightDeltas[i];

                }
            }

            //apply difference in position
            for (int i = 0; i < xpositions.Length; i++)
            {
                if (i > 0)
                {
                    ypositions[i - 1] += leftDeltas[i];
                }
                if (i < xpositions.Length - 1)
                {
                    ypositions[i + 1] += rightDeltas[i];
                }
            }

            //we update meshes to reflect
            UpdateMeshes();
        }
    }
}

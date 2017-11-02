using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResource : MonoBehaviour {

    //public variables to be used in unity scene editor (should show up automatically)
    public float capacity = 1000;
    // this is to store how much of a resource an object can hold.
    // another way we can do this is by making it so that the amount of resources
    // an object can hold is infinite. We could then restrict the amount of units that can
    // interact with it.

    // if we have a certain # capacity tho, we could destroy the object once resourceLeft = 0
    // or we could have it gradually refill back to full.
    public float amount = 1;
    //delete the above line when we make units that can take different amounts of resources.
    //this is for bug testing purposes alone.

    protected float resourceLeft;
    //protected ResourceType resourceType;
    //ResourceType should be a class that contains a type of resource. I may take this out
    //if we only have one kind of resource.


        // this will be able to be overriden by sub resource classes if we want.
        // for now, it's just going to be a normal, unoverridable class.
        // If we implement multiple resource types, we'll need to override this.
    //protected override void Start()
    void Start ()
    {
      //  base.Start();
        resourceLeft = capacity;
    }



    public void Remove(float amount)
    {
        resourceLeft -= amount;
        if(resourceLeft < 0)
        {
            resourceLeft = 0;
        }
    }

    //this returns true or false depending on if the resourceLeft float is at 0 or below.
    //You would call this *somewhere* (probably in resourceUnit) prior to trying to take any
    //resources from the object this is attached to. If it returns true, don't try to take anything.
    //else, go for it.
    public bool isEmpty()
    {
        return resourceLeft <= 0;
    }


/* so, this function should destroy whatever object this script is attached to when it hits 0 resourceLeft
 * as of now, this does not work. Everything else should work though, so it's whatever. I'll figure it out later.
 * 
 *    public void destroyResource(object obj)
    {
        if(resourceLeft == 0)
        {
            destroy(obj);
        }
    }
    */
}

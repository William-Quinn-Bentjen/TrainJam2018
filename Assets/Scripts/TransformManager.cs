using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour {
    public List<GameObject> Children = new List<GameObject>();
    private void Reset()
    {
        Children.Clear();
        ExploreRootObject(transform, Children);
    }
    public void MatchTransforms(TransformManager Original)
    {
        if (Original.Children.Count == Children.Count)
        {
            Transform Child;
            Transform OriginalChild; 
            for (int i = 0; i < Children.Count; i++)
            {
                Child = Children[i].transform;
                OriginalChild = Original.Children[i].transform;
                Child.position = OriginalChild.position;
                Child.rotation = OriginalChild.rotation;
                Child.localScale = OriginalChild.localScale;
            }
        }
    }




    /// <summary>
    /// Adds all children of a root GameObject to the list of all children
    /// </summary>
    /// <param name="rootTransform">The Transform of the GameObject at the root of the scene (is a child of no one)</param>
    /// <param name="allChildren">A reference to the list of all children in the scene</param>
    private void ExploreRootObject(Transform rootTransform, List<GameObject> allChildren)
    {
        List<Transform> unexplored = new List<Transform>();
        unexplored.Add(rootTransform);
        while (unexplored.Count > 0)
        {
            unexplored.AddRange(Explore(unexplored[0], allChildren));
            unexplored.RemoveAt(0);
        }
    }
    /// <summary>
    /// Adds children of parent object to the all children list and returns the children of each child
    /// </summary>
    /// <param name="parent">The parent that will have it's children explored</param>
    /// <param name="allChildren">A reference to the list of all children in the scene</param>
    /// <returns></returns>
    private List<Transform> Explore(Transform parent, List<GameObject> allChildren)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
            allChildren.Add(child.gameObject);
        }
        return children;
    }
}

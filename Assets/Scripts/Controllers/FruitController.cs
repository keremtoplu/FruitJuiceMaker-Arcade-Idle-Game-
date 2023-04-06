using System.Collections;
using System.Collections.Generic;
using Shadout.Controllers;
using UnityEngine;

public class FruitController : Item
{
    private bool isRipe = false;
    private TreeController tree;
    
    public bool IsRipe => isRipe;

    public void Setup(TreeController tree)
    {
        this.tree = tree;
        tree.TakePosition(this);
        LeanTween.scale(gameObject, Vector3.one, tree.GrowUpTime).setOnComplete(()=> 
        {
            isRipe = true;
            tree.AddCollectables(this);
        });
    }
}

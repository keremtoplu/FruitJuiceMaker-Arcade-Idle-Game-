using Shadout.Controllers;

public interface IStacker
{
    StackController Stack { get; set; }
    int Capacity  { get; set; }
    void CollectFruit(TreeController treeController);
}

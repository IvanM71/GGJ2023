
public class RootsModel
{
    public Root[,] view;
    public Root[,] buffer;

    public void Swap()
    {
        view = buffer;
        buffer = (Root[,])view.Clone();
    }
}

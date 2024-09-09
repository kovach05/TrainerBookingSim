namespace BusinessLogic.Interface;

public interface ILoader<T>
{
    List<T> Load(string filePath);
}
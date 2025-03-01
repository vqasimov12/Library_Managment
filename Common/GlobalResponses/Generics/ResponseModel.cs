using Common.GlobalResopnses;

namespace Common.GlobalResponses.Generics;
public class ResponseModel<T> : ResponseModel
{
    public T? Data { get; set; }

    public ResponseModel(List<string> message) : base(message)
    {

    }

    public ResponseModel()
    {

    }
}
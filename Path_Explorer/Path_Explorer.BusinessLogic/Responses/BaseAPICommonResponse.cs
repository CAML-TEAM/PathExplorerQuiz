namespace Path_Explorer.DataTransferObject.Common;
public abstract class BaseAPICommonResponse {
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string StatusCode { get; set; } = "CODE_EXE_SUCCESS";

    public int Code { get; set; } = 200;
}

namespace BE
{
    public class ResultResponse<T>
    {
        public Result<T> ResultSingle { get; set; }

        /// <summary>
        /// Constructor to single item response
        /// </summary>
        /// <param name="item"></param>
        public ResultResponse(T item)
        {
            ResultSingle = new Result<T>
            {
                IsSuccess = item != null,
                Data = item
            };
        }
    }
}

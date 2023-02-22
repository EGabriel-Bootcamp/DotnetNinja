using System;
namespace BankApp
{
	public class ObjectResult
	{
		public virtual bool Success { get; set; }
		public virtual string Message { get; set; }
        public string? UserName { get; set; } = null;
    }

    public class SuccessResult : ObjectResult
    {
		public override bool Success { get; set; } = true;
        public override required string Message { get; set; }
    }

    public class FailureResult : ObjectResult
    {
        public override bool Success { get; set; } = false;
        public override required string Message { get; set; }
    }
}


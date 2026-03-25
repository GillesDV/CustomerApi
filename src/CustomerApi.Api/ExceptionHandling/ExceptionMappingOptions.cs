namespace CustomerApi.Api.ExceptionHandling
{
    public sealed class ExceptionMappingOptions
    {
        private readonly Dictionary<Type, int> _mappings = new();

        public void Map<TException>(int statusCode) where TException : Exception
        {
            _mappings[typeof(TException)] = statusCode;
        }

        public bool TryGetStatusCode(Exception exception, out int statusCode)
        {
            var type = exception.GetType();

            // Exact match first
            if (_mappings.TryGetValue(type, out statusCode))
                return true;

            // Fallback: handle inheritance (very useful)
            var match = _mappings
                .FirstOrDefault(x => x.Key.IsAssignableFrom(type));

            if (match.Key != null)
            {
                statusCode = match.Value;
                return true;
            }

            statusCode = StatusCodes.Status500InternalServerError;
            return false;
        }
    }

}

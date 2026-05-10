export const mapIngestionError = (
  err: any
): string => {

  if (!err.response) {
    return 'C# API is unreachable.';
  }

  if (err.response.status === 409) {
    return 'Duplicate Activity detected.';
  }

  return (
    err.response?.data?.message ||
    'Unexpected ingestion failure.'
  );
};
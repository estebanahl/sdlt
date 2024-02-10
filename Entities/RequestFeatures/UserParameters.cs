using sdlt.Entities.RequestFeatures;

namespace sdlt.Entities.RequestFeatures;

public class UserParameters : RequestParameters
{
    public UserParameters() => OrderBy = "UserName";
}

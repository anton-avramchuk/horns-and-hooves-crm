namespace HornsAndHoovesCrm.Modules.OpenApi.Services;

public interface IClusterAddressProvider
{
    IEnumerable<string> GetClusterAddresses();
}
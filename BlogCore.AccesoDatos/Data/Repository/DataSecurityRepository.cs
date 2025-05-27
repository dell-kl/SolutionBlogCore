using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.DataProtection;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class DataSecurityRepository : IDataSecurityRepository
    {
        private readonly IDataProtector _protector;
        
        public DataSecurityRepository(IDataProtectionProvider protectorProvider) 
        { 
            _protector = protectorProvider.CreateProtector("BlogCore.DataSecurityRepository.c29sdXRpb24gYmxvZyBjb3JlIHBhcmEgcHJvdGVjdG9yIGRlIGRhdG9zLCBlc3RlIG5vcyBwZXJt\r\naXRpcmEgcHJvdGVnZXIgY2llcnRvIGNvbnRlbmlkbyBxdWUgbG8gdmFtb3MgYSByZW5kZXJpemFy\r\nIGVuIG51ZXN0cmEgd2ViLiBTZXJhIGFsZ28gbWVyYW1lbnRlIGltcG9ydGFudGUK");
        }

        public string desencriptarDatos(string encriptado) => _protector.Unprotect(encriptado);
        public string encriptarDatos(string entrada) => _protector.Protect(entrada);
    }
}

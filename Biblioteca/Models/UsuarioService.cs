using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public void Inserir(Usuario u)
        {
            using(BibliotecaContext uu = new BibliotecaContext())
            {
                u.senhaUsuario = GenerateHashPassword(u.senhaUsuario);
                uu.Usuario.Add(u);
                uu.SaveChanges();
            }
        }

        public void Atualizar(Usuario u)
        {
            using(BibliotecaContext uu = new BibliotecaContext())
            {
                Usuario usuario = uu.Usuario.Find(u.Id);
                usuario.loginUsuario = u.loginUsuario;
                usuario.senhaUsuario = u.senhaUsuario;

                uu.SaveChanges();
            }
        }
        
        public Usuario ObterPorId(int id)
        {
            using(BibliotecaContext uu = new BibliotecaContext())
            {
                return uu.Usuario.Find(id);
            }
        }

        public ICollection <Usuario> Listar(){
            using(BibliotecaContext bc = new BibliotecaContext()){
                ICollection<Usuario> consulta = bc.Usuario.ToList();
                return consulta;
            }
        }

         public void Deletar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = bc.Usuario.Find(id);
                if (usuario.loginUsuario == "admin")
                {
                    return;

                }
                bc.Usuario.Remove(usuario);
                bc.SaveChanges();
            }
        }

        public static string GenerateHashPassword(string senha)
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(senha);

            array = hash.ComputeHash(array);

            var strHex = new StringBuilder();

            foreach (var item in array)
            {
                strHex.Append(item.ToString("x2"));
            }
            
            return strHex.ToString();
        }
    }
}
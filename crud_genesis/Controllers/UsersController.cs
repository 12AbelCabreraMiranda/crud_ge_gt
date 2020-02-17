using crud_genesis.Models;
using crud_genesis.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud_genesis.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        //Seleccion de tipo de usuario
        [HttpGet]
        public ActionResult NuevoUsuario()
        {
            var roles = new List<TipoUsuario>();
            using (var context = new TestGenesisContext())
            {
                roles = context.TipoUsuario.ToList();
            }
            ViewBag.TipoUsuario = new SelectList(roles, "TipoUsuarioId", "NombreTipo");

            var empleadoViewModel = new EmpleadoViewModel();
            return View(empleadoViewModel);
        }
        //Nuevo usuario
        [HttpPost]
        public ActionResult NuevoUsuario(EmpleadoViewModel model)
        {                        
            try
            {
                if (ModelState.IsValid)
                {
                    using (TestGenesisContext db = new TestGenesisContext())
                    {                        
                        var estadoUser = "1";

                        var userVal = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == model.NombreUsuario);

                        if ( userVal != null)
                        {
                            //Si el usuario ya existe, entonces no registra dato.
                        }
                        else
                        {
                            var empleado = new Empleado()
                            {// P. DE TABLA      Prop. DEL MODELO
                                NombreEmpleado = model.NombreEmpleado,
                                Direccion = model.Direccion
                            };
                            db.Empleado.Add(empleado);
                            db.SaveChanges();

                            var usuario = new Usuarios()
                            {
                                NombreUsuario = model.NombreUsuario,
                                ContraseniaUsuario = model.ContraseniaUsuario,
                                Estado = estadoUser,
                                EmpleadoId = empleado.EmpleadoId,
                                TipoUsuarioId = model.TipoUsuarioId
                            };
                            db.Usuarios.Add(usuario);
                            db.SaveChanges();
                        }
                    }
                    return Redirect("../Crud/DatosUsuarios");

                }

                return View(model);
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.Message);
            }            
        }     
        //Update Users          
        public ActionResult UpdateUsers(int id)
        {
            UsuarioUpdateViewModel model = new UsuarioUpdateViewModel();
            using (TestGenesisContext db = new TestGenesisContext())
            {

                var usuE = (from u in db.Usuarios
                            join e in db.Empleado
                            on u.EmpleadoId equals e.EmpleadoId

                            where u.UsuariosId == id
                            select new UsuarioUpdateViewModel
                            {
                                EmpleadoId = e.EmpleadoId,
                                UsuariosId = u.UsuariosId,
                                NombreEmpleado = e.NombreEmpleado,
                                Direccion = e.Direccion,
                                NombreUsuario = u.NombreUsuario,
                                ContraseniaUsuario = u.ContraseniaUsuario
                            }).ToList();

                model.EmpleadoId = usuE[0].EmpleadoId;
                model.UsuariosId = usuE[0].UsuariosId;
                model.NombreEmpleado = usuE[0].NombreEmpleado;
                model.Direccion = usuE[0].Direccion;
                model.NombreUsuario = usuE[0].NombreUsuario;
                model.ContraseniaUsuario = usuE[0].ContraseniaUsuario;

                return View(model);
            }
        }
        [HttpPost]
        public ActionResult UpdateUsers(UsuarioUpdateViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (TestGenesisContext db = new TestGenesisContext())
                    {
                        var empleado = db.Empleado.FirstOrDefault(e => e.EmpleadoId == model.EmpleadoId);
                        var usuario = db.Usuarios.FirstOrDefault(u => u.UsuariosId == model.UsuariosId);
                        
                        if (empleado != null && usuario != null)
                        {

                            empleado.NombreEmpleado = model.NombreEmpleado;
                            empleado.Direccion = model.Direccion;
                            usuario.NombreUsuario = model.NombreUsuario;
                            usuario.ContraseniaUsuario = model.ContraseniaUsuario;


                            db.Entry(empleado).State = EntityState.Modified;
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    return Redirect("/Crud/DatosUsuarios");
                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //ELIMINAR        
        public ActionResult Delete(int id)
        {
            using (TestGenesisContext db = new TestGenesisContext())
            {

                var oTabla = db.Usuarios.Find(id);
                oTabla.Estado = "0";


                db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect("/Crud/DatosUsuarios");
        }





    }
}
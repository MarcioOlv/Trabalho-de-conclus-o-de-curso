using Gerenciador_de_Consultas_Médicas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gerenciador_de_Consultas_Médicas.Controllers
{
    public class AccountController : Controller
    {

        DominioContainer db = new DominioContainer();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //teste
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel usuario)
        {
            using (db)
            {
                var userPaciente = db.pacientesSet.Where(u => u.email == usuario.emailUsuario && u.senha == usuario.senhaUsuario).FirstOrDefault();
                if (userPaciente != null )
                {
                    Session["idUsuario"] = userPaciente.idPaciente.ToString();
                    Session["nomeUsuario"] = userPaciente.nome.ToString();
                    Session["tipoUsuario"] = "paciente";
                    return RedirectToAction("Index", "consultas");
                }
                else
                {
                    var userMedico = db.medicosSet.Where(u => u.email == usuario.emailUsuario && u.senha == usuario.senhaUsuario).FirstOrDefault();
                    if (userMedico != null)
                    {
                        Session["idUsuario"] = userMedico.idMedico.ToString();
                        Session["nomeUsuario"] = userMedico.nome.ToString();
                        Session["tipoUsuario"] = "medico";
                        Session["idClinica"] = userMedico.clinicas_idClinica.ToString();
                        Session["adm"] = userMedico.adm.ToString();
                        if (Convert.ToInt32(Session["adm"]) == 1)
                        {
                            Session["tipoUsuario"] = "Adm";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                            return RedirectToAction("Index", "agenda");                        
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email e/ou senha incorretos");
                    }
                }
            }
            return View();

       }

        public ActionResult Sair()
        {
            Session["idUsuario"] = null;
            Session["nomeUsuario"] = null;
            Session["tipoUsuario"] = null;
            Session["adm"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult confirmarDados()
        {
            return View();
        }

        [HttpPost]
        public ActionResult confirmarDados(AccountViewModel usuario)
        {
            using (db)
            {
                var userPaciente = db.pacientesSet.Where(u => u.email == usuario.emailUsuario && u.cpf == usuario.cpf).FirstOrDefault();
                if (userPaciente != null)
                {
                    return RedirectToAction("redefinirSenha",userPaciente);
                }
                else
                {
                    ModelState.AddModelError("", "Dados inválidos");
                }
            }
            return View();
        }

        public ActionResult redefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult redefinirSenha([Bind(Include = "senha")]pacientes userPaciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(userPaciente).State = EntityState.Modified;
                    db.SaveChanges();
                    return View("_LayoutDadosAlterados");
                }
                return View(userPaciente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public ActionResult Edit([Bind(Include = "idPaciente,nome,cpf,endereco,cidade,estado,telefone,celular,email,senha,confirmarSenha,convenios_idConvenio")] pacientes pacientes)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(pacientes).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.convenios_idConvenio = new SelectList(db.conveniosSet, "idConvenio", "descricao", pacientes.convenios_idConvenio);
        //        return View(pacientes);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

    }
}
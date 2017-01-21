using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gerenciador_de_Consultas_Médicas.Models;

namespace Gerenciador_de_Consultas_Médicas.Controllers
{
    public class pacientesController : Controller
    {
        private DominioContainer db = new DominioContainer();

        // GET: pacientes
        public ActionResult Index()
        {
            
            int id = Convert.ToInt16(Session["idUsuario"]);
            var pacientesSet = db.pacientesSet.Where(p => p.idPaciente == id);
            return View(pacientesSet);
        }

        // GET: pacientes/Details/5
        public ActionResult Details()
        {
            int id = Convert.ToInt16(Session["idUsuario"]);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            pacientes pacientes = db.pacientesSet.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // GET: pacientes/Create
        public ActionResult Create()
        {
            clinicasController estados = new clinicasController();

            ViewBag.listaEstados = estados.listaEstados();
            ViewBag.convenios = new SelectList(db.conveniosSet, "idConvenio", "descricao");
            return View();
        }

        // POST: pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pacientes pacientes, string[] estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.pacientesSet.Add(pacientes);
                    db.SaveChanges();
                    return View("_LayoutCadastroRealizado");
                }
            }
            catch (Exception)
            {
                throw;
            }

            clinicasController estados = new clinicasController();

            ViewBag.listaEstados = estados.listaEstados();
            ViewBag.convenios_idConvenio = new SelectList(db.conveniosSet, "idConvenio", "descricao", pacientes.convenios_idConvenio);
            return View(pacientes);
        }

        // GET: pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacientes pacientes = db.pacientesSet.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.convenios_idConvenio = new SelectList(db.conveniosSet, "idConvenio", "descricao", pacientes.convenios_idConvenio);
            return View(pacientes);
        }

        // POST: pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,nome,cpf,endereco,cidade,estado,telefone,celular,email,senha,confirmarSenha,convenios_idConvenio")] pacientes pacientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pacientes).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.convenios_idConvenio = new SelectList(db.conveniosSet, "idConvenio", "descricao", pacientes.convenios_idConvenio);
                return View(pacientes);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // GET: pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacientes pacientes = db.pacientesSet.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pacientes pacientes = db.pacientesSet.Find(id);
            db.pacientesSet.Remove(pacientes);
            db.SaveChanges();
            return RedirectToAction("Sair", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

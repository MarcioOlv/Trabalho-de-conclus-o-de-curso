using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gerenciador_de_Consultas_Médicas.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Gerenciador_de_Consultas_Médicas.Controllers
{
    public class clinicasController : Controller
    {
        private DominioContainer db = new DominioContainer();

        // GET: clinicas
        public ActionResult Index()
        {
            int id = Convert.ToInt16(Session["idClinica"]);
            var clinica = db.clinicasSet.Where(c => c.idClinica == id);
            return View(clinica);
        }

        // GET: clinicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clinicas clinicas = db.clinicasSet.Find(id);
            if (clinicas == null)
            {
                return HttpNotFound();
            }
            return View(clinicas);
        }

        // GET: clinicas/Create
        public ActionResult Create()
        {
            ViewBag.ds_especialidades = new MultiSelectList(db.especialidadesSet, "ds_especialidade", "ds_especialidade");
            ViewBag.ds_convenios = new MultiSelectList(db.conveniosSet, "descricao", "descricao");
            return View();
        }

        // POST: clinicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idClinica,nome,cnpj,endereco,complemento,bairro,cep,cidade,estadoClinica,email,telefone,fax,hrAtendimento,duracaoConsultas,especialidades,convenios")] clinicas clinicas)
        {
            if (ModelState.IsValid)
            {
                db.clinicasSet.Add(clinicas);
                db.SaveChanges();
                Session["idClinica"] = clinicas.idClinica;
                return RedirectToAction("Create", "medicos");
            }

            return View(clinicas);
        }

        // GET: clinicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clinicas clinicas = db.clinicasSet.Find(id);
            if (clinicas == null)
            {
                return HttpNotFound();
            }
            return View(clinicas);
        }

        // POST: clinicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idClinica,nome,cnpj,endereco,complemento,bairro,cep,cidade,estadoClinica,email,telefone,fax,hrAtendimento,duracaoConsultas,especialidades,convenios")] clinicas clinicas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinicas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinicas);
        }

        // GET: clinicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            clinicas clinicas = db.clinicasSet.Find(id);
            if (clinicas == null)
            {
                return HttpNotFound();
            }
            return View(clinicas);
        }

        // POST: clinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            clinicas clinicas = db.clinicasSet.Find(id);
            db.clinicasSet.Remove(clinicas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult pesquisarClinicas()
        {
            ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "ds_especialidade", "ds_especialidade");   
            return View("pesquisarClinicas");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pesquisarClinicas(ClinicasPacMedViewModel clinica)
        {
            var idUsuario = Convert.ToInt16(Session["idUsuario"]);
            var query = from c in db.clinicasSet where c.especialidades == clinica.especialidades
                        from p in db.pacientesSet where p.idPaciente == idUsuario
                        select new ClinicasPacMedViewModel()
                                {
                                    idClinica = c.idClinica,
                                    nomeClinica = c.nome,
                                    enderecoClinica = c.endereco,
                                    bairroClinica = c.bairro,
                                    cepClinica = c.cep,
                                    cidadeClinica = c.cidade,
                                    estadoClinica = c.estadoClinica,
                                    emailClinica = c.email,
                                    telefoneClinica = c.telefone,
                                    hrAtendimento = c.hrAtendimento,
                                    especialidades = c.especialidades,
                                    convenios = c.convenios,
                                    nomePaciente = p.nome,
                                    enderecoPaciente = p.endereco,
                                    cidadePaciente = p.cidade
                                };

            List < ClinicasPacMedViewModel > listaCli = query.ToList();

            ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade");
            return View("listaClinicas", listaCli.ToList());
        }

        public ActionResult pesquisarMedicos(int id)
        {
            ClinicasPacMedViewModel idCli = new ClinicasPacMedViewModel();
            idCli.idClinica = id;
            return RedirectToAction("listarMedicos", "medicos", idCli);
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

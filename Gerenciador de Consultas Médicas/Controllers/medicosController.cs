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
    public class medicosController : Controller
    {
        private DominioContainer db = new DominioContainer();

        // GET: medicos
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt16(Session["idUsuario"]);
            int idClinica = Convert.ToInt16(Session["idClinica"]);

            if (Convert.ToInt16(Session["adm"]) == 0)
            {
                var medico = db.medicosSet.Where(m => m.idMedico == idUsuario);
                return View(medico.ToList());
            }
            else
            {
                var medico = db.medicosSet.Where(m => m.clinicas_idClinica == idClinica);
                return View(medico.ToList());
            }
        }

        // GET: medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                id = Convert.ToInt16(Session["idUsuario"]);

            medicos medicos = db.medicosSet.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }

            return View(medicos);
        }

        // GET: medicos/Create
        public ActionResult Create()
        {            
            ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade");
            return View();
        }

        // POST: medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMedico,nome,email,senha,confirmarSenha,preco,situacao,especialidades_idEspecialidade,clinicas_idClinica")] medicos medicos)
        {
            medicos.clinicas_idClinica = Convert.ToInt16(Session["idClinica"]);
            medicos.situacao = 1;
            if (Session["idUsuario"] == null)
                medicos.adm = 1;
            else
                medicos.adm = 0;

            if (ModelState.IsValid)
            {
                db.medicosSet.Add(medicos);
                db.SaveChanges();

                if (Session["idUsuario"] != null)
                    return RedirectToAction("Index");
                else
                    return View("_LayoutCadastroRealizado");
            }

            //ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade", medicos.especialidades_idEspecialidade);
            ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade");
            ViewBag.clinicas_idClinica = new SelectList(db.clinicasSet, "idClinica", "nome", medicos.clinicas_idClinica);
            return View(medicos);
        }

        // GET: medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medicos medicos = db.medicosSet.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            //ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade", medicos.especialidades_idEspecialidade);
            ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade");
            ViewBag.clinicas_idClinica = new SelectList(db.clinicasSet, "idClinica", "nome", medicos.clinicas_idClinica);
            return View(medicos);
        }

        // POST: medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMedico,nome,email,senha,confirmarSenha,preco,situacao,especialidades_idEspecialidade")] medicos medicos)
        //[Bind(Include = "idMedico,nome,email,senha,preco,situacao")]
        {
            int id = Convert.ToInt16(Session["idClinica"]);
            medicos.clinicas_idClinica = id;
            
            if (ModelState.IsValid)
            {
                if (Convert.ToString(Session["tipoUsuario"]) == "Adm" && medicos.idMedico == Convert.ToUInt16(Session["idUsuario"]))
                    medicos.adm = 1;
                else
                    medicos.adm = 0;
                db.Entry(medicos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
           
            ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade", medicos.especialidades_idEspecialidade);
            ViewBag.clinicas_idClinica = new SelectList(db.clinicasSet, "idClinica", "nome", medicos.clinicas_idClinica);
            return View(medicos);
        }

        
        // GET: medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medicos medicos = db.medicosSet.Find(id);
            if (medicos == null)
            {
                return HttpNotFound();
            }
            return View(medicos);
        }

        // POST: medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            medicos medicos = db.medicosSet.Find(id);
            db.medicosSet.Remove(medicos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //listar os medicos da clínica selecionada, com os dados de sua agenda.
        public ActionResult listarMedicos(ClinicasPacMedViewModel cli)
        {
            var query = (from m in db.medicosSet
                         where m.clinicas_idClinica == cli.idClinica
                         from e in db.especialidadesSet
                         where e.idEspecialidade == m.especialidades_idEspecialidade
                         //group m by m.nome into z
                         //from a in db.agendaSet
                         //where a.medicos_idMedico == m.idMedico && a.horarioAgendado == null
                         select new perfilMedicoViewModel
                         {
                             //idClinica = m.clinicas_idClinica,
                             idMedico = m.idMedico,
                             nome = m.nome,
                             especialidade = e.ds_especialidade,
                             preco = m.preco,
                             emailMedico = m.email
                             //data = a.data,
                             //horarios = a.horarioAtendimento
                         });

            return View(query.ToList());
        }

        public ActionResult criarAgenda(int id)
        {
            agendaMedicoViewModel idMed = new agendaMedicoViewModel();
            idMed.idMedico = id;

            return RedirectToAction("IndexAdm", "agenda", idMed);
        }

        public ActionResult verificarAgenda(int idMedico)
        {
            infParaAgendaEConsultaViewModel agenda = new infParaAgendaEConsultaViewModel();
            agenda.idMedico = idMedico;

            return RedirectToAction("verificarAgenda", "agenda", agenda);
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

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
    public class consultasController : Controller
    {
        private DominioContainer db = new DominioContainer();

        // GET: consultas
        public ActionResult Index()
        {
            agendaController agenda = new agendaController();
            agenda.deletarAgendaAntiga();

            var idUsuario = Convert.ToInt16(Session["idUsuario"]);
            var consultasSet = db.consultasSet.Where(c => c.pacientes_idPaciente == idUsuario && c.check_out != 1);
            return View(consultasSet.ToList());
        }

        // GET: consultas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consultas consultas = db.consultasSet.Find(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            return View(consultas);
        }

        // GET: consultas/Create
        public ActionResult Create()
        {
            ViewBag.especialidades_idEspecialidade = new SelectList(db.especialidadesSet, "idEspecialidade", "ds_especialidade");
            return View();
        }

        public ActionResult confirmarConsulta(infParaAgendaEConsultaViewModel marcar)
        {
            return View(marcar);
        }

        public ActionResult confirmarDados(string data, string horario, int cli, int med)
        {
            var idPac = Convert.ToInt16(Session["idUsuario"]);
            consultas consConfirmada = new consultas();
            consConfirmada.pacientes_idPaciente = idPac;
            consConfirmada.data = data;
            consConfirmada.horario = horario;
            consConfirmada.clinicas_idClinica = cli;
            consConfirmada.medicos_idMedico = med;

            var alterarAgenda = db.agendaSet.Where(a => a.data == data & a.horarioAtendimento == horario & 
                                                a.medicos_idMedico == med).Single();
            alterarAgenda.horarioAgendado = "Agendado";
            
            db.consultasSet.Add(consConfirmada);
            db.SaveChanges();
            return View("consultaAgendada");
        }


        // GET: consultas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consultas consultas = db.consultasSet.Find(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            ViewBag.pacientes_idPaciente = new SelectList(db.pacientesSet, "idPaciente", "nome", consultas.pacientes_idPaciente);
            ViewBag.medicos_idMedico = new SelectList(db.medicosSet, "idMedico", "nome", consultas.medicos_idMedico);
            ViewBag.clinicas_idClinica = new SelectList(db.clinicasSet, "idClinica", "nome", consultas.clinicas_idClinica);
            return View(consultas);
        }

        // POST: consultas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult reagendar(int? id, int idMed, string hora, string data)
        {
            var cancelarAgenda = db.agendaSet.Where(a => a.data == data & a.horarioAtendimento == hora &
                                                a.medicos_idMedico == idMed).Single();
            cancelarAgenda.horarioAgendado = null;

            consultas consulta = db.consultasSet.Find(id);
            db.consultasSet.Remove(consulta);
            db.SaveChanges();
            return RedirectToAction("pesquisarClinicas", "clinicas");
        }

        // GET: consultas/Delete/5
        public ActionResult Delete(int? id, int idMed, string hora, string data)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consultas consultas = db.consultasSet.Find(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            return View(consultas);
        }

        // POST: consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idMed, string hora, string data)
        {
            consultas consultas = db.consultasSet.Find(id);

            var alterarAgenda = db.agendaSet.Where(a => a.data == data & a.horarioAtendimento == hora &
                                                a.medicos_idMedico == idMed).Single();
            alterarAgenda.horarioAgendado = null;

            db.consultasSet.Remove(consultas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult checkIn(int idConsulta, int idMedico, string data, string hora)
        {
            var consulta = db.consultasSet.Where(c => c.idConsulta == idConsulta).Single();
            consulta.check_in = 1;

            var agenda = db.agendaSet.Where(a => a.medicos_idMedico == idMedico && a.data == data && a.horarioAtendimento == hora).Single();
            agenda.horarioAgendado = "Check-in realizado";

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult checkOut(int idConsulta, int idMedico, string data, string hora)
        {
            var consulta = db.consultasSet.Where(c => c.idConsulta == idConsulta).Single();
            consulta.check_out = 1;

            var agenda = db.agendaSet.Where(a => a.medicos_idMedico == idMedico && a.data == data && a.horarioAtendimento == hora).Single();
            agenda.horarioAgendado = "Check-out realizado";

            db.SaveChanges();
            
            infAvaliacaoViewModel aval = new infAvaliacaoViewModel();
            aval.idMedico = idMedico;
            
            return RedirectToAction("Create", "avaliacoes", aval);
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

using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gerenciador_de_Consultas_Médicas.Models;
using PagedList;

namespace Gerenciador_de_Consultas_Médicas.Controllers
{
    public class agendaController : Controller
    {
        private DominioContainer db = new DominioContainer();

        // GET: agenda
        public ActionResult Index(int? page)
        {
            int id = Convert.ToInt16(Session["idUsuario"]);
            var agendaSet = db.agendaSet.Where(a => a.medicos_idMedico == id && a.horarioAgendado != "Check-out realizado").OrderBy(a => a.data);

            //quantidade de itens
            int pageSize = 10;
            //numero da pag, se for nulo retorna 1
            int pageNumber = (page ?? 1);

            return View(agendaSet.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult verificarAgenda(infParaAgendaEConsultaViewModel medico,int? idMedico, int? page)
        {
            
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (medico == null)
            {
                var query = from a in db.agendaSet
                            where a.medicos_idMedico == idMedico && a.horarioAgendado == null
                            from m in db.medicosSet
                            where m.idMedico == medico.idMedico
                            orderby a.data
                            select new infParaAgendaEConsultaViewModel
                            {
                                idClinica = m.clinicas_idClinica,
                                idMedico = m.idMedico,
                                nomeMedico = m.nome,
                                data = a.data,
                                horarios = a.horarioAtendimento
                            };

                ViewBag.idMedico = idMedico;

                return View(query.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var query = from a in db.agendaSet
                            where a.medicos_idMedico == medico.idMedico && a.horarioAgendado == null
                            from m in db.medicosSet
                            where m.idMedico == medico.idMedico
                            orderby a.data
                            select new infParaAgendaEConsultaViewModel
                            {
                                idClinica = m.clinicas_idClinica,
                                idMedico = m.idMedico,
                                nomeMedico = m.nome,
                                data = a.data,
                                horarios = a.horarioAtendimento
                            };

                ViewBag.idMedico = medico.idMedico;

                return View(query.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult confirmarConsulta(int idMedico, string nomeMedico, string data, string horario, int idClinica)
        {
            infParaAgendaEConsultaViewModel marcar = new infParaAgendaEConsultaViewModel();
            marcar.nomeMedico = nomeMedico;
            marcar.idMedico = idMedico;
            marcar.data = data;
            marcar.horarios = horario;
            marcar.idClinica = idClinica;

            return RedirectToAction("confirmarConsulta", "consultas", marcar);
        }

        public ActionResult IndexAdm(agendaMedicoViewModel idMed)
        {
            var agendaSet = db.agendaSet.Where(a => a.medicos_idMedico == idMed.idMedico && a.horarioAgendado != "Check-out realizado");
            return View("Index", agendaSet);
        }

        // GET: agenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agenda agenda = db.agendaSet.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // GET: agenda/Create
        public ActionResult Create()
        {
            ViewBag.medicos_idMedico = new SelectList(db.medicosSet, "idMedico", "nome");
            
            return View();
        }

        // POST: agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(agendaMedicoViewModel dadosAgenda)
        {//[Bind(Include = "idAgenda,diasSemana,horarioAtendimento,medicos_idMedico")] 

            var inicio = dadosAgenda.inicioAtendimento;
            var fim = dadosAgenda.fimAtendimento;
            TimeSpan intervalo = TimeSpan.FromMinutes(30);

            int id = Convert.ToInt16(Session["idUsuario"]);
            agenda agenda = new agenda();
            agenda.medicos_idMedico = id;
            agenda.data = dadosAgenda.data;

            if (ModelState.IsValid && inicio < fim)
            {
                while (inicio <= fim)
                {
                    agenda.horarioAtendimento = Convert.ToString(inicio);
                    inicio = inicio + intervalo;
                    db.agendaSet.Add(agenda);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            
            //ViewBag.medicos_idMedico = new SelectList(db.medicosSet, "idMedico", "nome", agenda.medicos_idMedico);
            return View(dadosAgenda);
        }

        // GET: agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agenda agenda = db.agendaSet.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.medicos_idMedico = new SelectList(db.medicosSet, "idMedico", "nome", agenda.medicos_idMedico);
            return View(agenda);
        }

        // POST: agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(agenda agenda)
        {//[Bind(Include = "idAgenda,diasSemana,horarioAtendimento,horarioAgendado,medicos_idMedico")]
            int id = Convert.ToInt16(Session["idUsuario"]);
            agenda.medicos_idMedico = id;
            if (ModelState.IsValid)
            {
                db.Entry(agenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.medicos_idMedico = new SelectList(db.medicosSet, "idMedico", "nome", agenda.medicos_idMedico);
            return View(agenda);
        }

        // GET: agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agenda agenda = db.agendaSet.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            agenda agenda = db.agendaSet.Find(id);
            db.agendaSet.Remove(agenda);
            db.SaveChanges();
            return RedirectToAction("Index");
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

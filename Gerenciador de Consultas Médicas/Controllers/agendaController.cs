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
        public ActionResult Index(int? idMedico, int? page)
        {
            deletarAgendaAntiga();

            //quantidade de itens
            int pageSize = 10;
            //numero da pag, se for nulo retorna 1
            int pageNumber = (page ?? 1);

            if (idMedico == null)
            {
                idMedico = Convert.ToInt16(Session["idUsuario"]);
            }
            
            var agendaSet = db.agendaSet.Where(a => a.medicos_idMedico == idMedico && a.horarioAgendado != "Check-out realizado").
            OrderByDescending(a => a.horarioAgendado).ThenBy(a => a.data);

            ViewBag.idMedico = idMedico;

            return View(agendaSet.ToPagedList(pageNumber, pageSize));
            
        }

        //Index de quando o Adm da clínica chama
        public ActionResult IndexAdm(agendaMedicoCadastroViewModel idMed)
        {
            int id = idMed.idMedico;

            return RedirectToAction("Index", new { idMedico = id });
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
        
        public void deletarAgendaAntiga()
        {
            var dateAndTime = DateTime.Now;
            var today = dateAndTime.Date;
            var agenda = db.agendaSet.ToList().
                Where(a => a.horarioAgendado == null && Convert.ToDateTime(a.data) < today).OrderBy(a => a.data);

            if (!agenda.Any())
                return;

            foreach(var a in agenda)
                db.agendaSet.Remove(a);

            db.SaveChanges();
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
        public ActionResult Create(agendaMedicoCadastroViewModel dadosAgenda)
        {
            var inicio = dadosAgenda.inicioAtendimento;
            var fim = dadosAgenda.fimAtendimento;

            int id = Convert.ToInt16(Session["idUsuario"]);
            agenda agenda = new agenda();
            agenda.medicos_idMedico = id;
            agenda.data = dadosAgenda.data;

            int idClinica = Convert.ToInt16(Session["idClinica"]);
            var duracaoConsultas = db.clinicasSet.Where(c => c.idClinica == idClinica).Select(c => c.duracaoConsultas).Single();
            TimeSpan intervalo = TimeSpan.FromMinutes(duracaoConsultas);

            if (ModelState.IsValid && inicio < fim)
            {
                while (inicio <= fim)
                {
                    agenda.horarioAtendimento = Convert.ToString(inicio);
                    inicio = inicio + intervalo;
                    db.agendaSet.Add(agenda);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", dadosAgenda.idMedico);
            }
            
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
            
            return View(agenda);
        }

        // POST: agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(agenda agenda)
        {
            int id = Convert.ToInt16(Session["idUsuario"]);
            agenda.medicos_idMedico = id;
            if (ModelState.IsValid)
            {
                db.Entry(agenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", agenda.medicos_idMedico);
            }
            
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
            return RedirectToAction("Index", agenda.medicos_idMedico);
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

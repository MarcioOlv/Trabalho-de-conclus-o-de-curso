using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gerenciador_de_Consultas_Médicas.Models;
using PagedList;

namespace Gerenciador_de_Consultas_Médicas.Controllers
{
    public class avaliacoesController : Controller
    {
        private DominioContainer db = new DominioContainer();

        // GET: avaliacoes
        public ActionResult Index(int idMedico, int? page)
        {
            var query = from m in db.medicosSet
                        where m.idMedico == idMedico
                        from a in db.avaliacoes
                        where a.medicos_idMedico == m.idMedico
                        from p in db.pacientesSet
                        where p.idPaciente == a.pacientes_idPaciente
                        orderby a.data
                        select new perfilMedicoViewModel
                        {
                            dataAvaliacao = a.data,
                            notas = a.notas,
                            comentarios = a.comentarios,
                            nomePacAvaliador = p.nome
                        };

            if (query.Count() != 0)
            {
                var mediaMed = db.avaliacoes.Where(m => m.medicos_idMedico == idMedico).OrderByDescending(m => m.idAvaliacao).Select(m => m.media).Take(1).Single();
                ViewBag.media = mediaMed;
            }

            ViewBag.idMedico = idMedico;
                
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(query.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: avaliacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            avaliacoes avaliacoes = db.avaliacoes.Find(id);
            if (avaliacoes == null)
            {
                return HttpNotFound();
            }
            return View(avaliacoes);
        }

        // GET: avaliacoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: avaliacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(infAvaliacaoViewModel avaliacao)
        {
            if (ModelState.IsValid)
            {
                var qtdAvals = db.avaliacoes.Count(q => q.medicos_idMedico == avaliacao.idMedico);
                
                if (qtdAvals != 0)
                {
                    var somaNotas = db.avaliacoes.Where(m => m.medicos_idMedico == avaliacao.idMedico).
                        GroupBy(m => m.medicos_idMedico).Select(n => n.Sum(m => m.notas)).Single();
                    
                    int total = Convert.ToInt16(somaNotas);
                    total += avaliacao.nota;
                    qtdAvals++;
                    avaliacao.media = Convert.ToDouble(total / qtdAvals);
                }
                else
                    avaliacao.media = avaliacao.nota;

                avaliacoes aval = new avaliacoes();
                aval.medicos_idMedico = avaliacao.idMedico;
                aval.notas = avaliacao.nota;
                aval.media = avaliacao.media;
                aval.data = DateTime.Today;
                aval.comentarios = avaliacao.comentario;
                aval.pacientes_idPaciente = Convert.ToInt16(Session["idUsuario"]);

                db.avaliacoes.Add(aval);
                db.SaveChanges();
                return RedirectToAction("Index", "consultas");
            }
            return View(avaliacao);
        }

        // GET: avaliacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            avaliacoes avaliacoes = db.avaliacoes.Find(id);
            if (avaliacoes == null)
            {
                return HttpNotFound();
            }
            ViewBag.medicos_idMedico = new SelectList(db.medicosSet, "idMedico", "nome", avaliacoes.medicos_idMedico);
            return View(avaliacoes);
        }

        // POST: avaliacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAvaliacao,notas,comentarios,media,data,medicos_idMedico")] avaliacoes avaliacoes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avaliacoes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.medicos_idMedico = new SelectList(db.medicosSet, "idMedico", "nome", avaliacoes.medicos_idMedico);
            return View(avaliacoes);
        }

        // GET: avaliacoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            avaliacoes avaliacoes = db.avaliacoes.Find(id);
            if (avaliacoes == null)
            {
                return HttpNotFound();
            }
            return View(avaliacoes);
        }

        // POST: avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            avaliacoes avaliacoes = db.avaliacoes.Find(id);
            db.avaliacoes.Remove(avaliacoes);
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

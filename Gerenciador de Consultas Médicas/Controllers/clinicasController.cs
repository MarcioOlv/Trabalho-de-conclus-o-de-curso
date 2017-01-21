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
using Gerenciador_de_Consultas_Médicas.Models.ViewModels;
using System.Collections;
using System.Text;

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
            ViewBag.listaEstados = listaEstados();
            ViewBag.ds_especialidades = new MultiSelectList(db.especialidadesSet, "ds_especialidade", "ds_especialidade");
            ViewBag.ds_convenios = new MultiSelectList(db.conveniosSet, "descricao", "descricao");
            
            return View();
        }

        // POST: clinicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(clinicas clinicas,string[] listaEspecialidades, string[] listaConvenios)
        {
            //[Bind(Include = "idClinica,nome,cnpj,endereco,complemento,bairro,cep,cidade,estadoClinica,email,telefone,fax,hrAtendimento,duracaoConsultas,especialidades,ds_convenios")]
            if (listaConvenios != null)
            {
                StringBuilder convenios = new StringBuilder();
                var ultimoCon = listaConvenios.Last();
                foreach (var convenio in listaConvenios)
                {
                    convenios.Append(convenio);
                    if (!convenio.Equals(ultimoCon))
                    {
                        convenios.Append(", ");
                    }
                }
                clinicas.convenios = Convert.ToString(convenios);
            }

            if (listaEspecialidades != null)
            {
                StringBuilder especialidades = new StringBuilder();
                var ultimaEsp = listaEspecialidades.Last();
                foreach (var especialidade in listaEspecialidades)
                {
                    especialidades.Append(especialidade);
                    if (!especialidade.Equals(ultimaEsp))
                    {
                        especialidades.Append(", ");
                    }
                }
                clinicas.especialidades = Convert.ToString(especialidades);
            }
            
            ModelState.Clear();
            
            if (TryValidateModel(clinicas))
            {
                db.clinicasSet.Add(clinicas);
                db.SaveChanges();
                Session["idClinica"] = clinicas.idClinica;
                return RedirectToAction("Create", "medicos");
            }

            ViewBag.ds_especialidades = new MultiSelectList(db.especialidadesSet, "ds_especialidade", "ds_especialidade");
            ViewBag.ds_convenios = new MultiSelectList(db.conveniosSet, "descricao", "descricao");
            ViewBag.listaEstados = listaEstados();
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
                                    convenios = Convert.ToString(c.convenios),
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

        public IList listaEstados()
        {
            List<SelectListItem> estados = new List<SelectListItem>();
            estados.Add(new SelectListItem() { Text = "AC", Value = "AC" });
            estados.Add(new SelectListItem() { Text = "AL", Value = "AL" });
            estados.Add(new SelectListItem() { Text = "AP", Value = "AP" });
            estados.Add(new SelectListItem() { Text = "AM", Value = "AM" });
            estados.Add(new SelectListItem() { Text = "BA", Value = "BA" });
            estados.Add(new SelectListItem() { Text = "CE", Value = "CE" });
            estados.Add(new SelectListItem() { Text = "DF", Value = "DF" });
            estados.Add(new SelectListItem() { Text = "ES", Value = "ES" });
            estados.Add(new SelectListItem() { Text = "GO", Value = "GO" });
            estados.Add(new SelectListItem() { Text = "MA", Value = "MA" });
            estados.Add(new SelectListItem() { Text = "MT", Value = "MT" });
            estados.Add(new SelectListItem() { Text = "MS", Value = "MS" });
            estados.Add(new SelectListItem() { Text = "MG", Value = "MG" });
            estados.Add(new SelectListItem() { Text = "PA", Value = "PA" });
            estados.Add(new SelectListItem() { Text = "PB", Value = "PB" });
            estados.Add(new SelectListItem() { Text = "PR", Value = "PR" });
            estados.Add(new SelectListItem() { Text = "PE", Value = "PE" });
            estados.Add(new SelectListItem() { Text = "PI", Value = "PI" });
            estados.Add(new SelectListItem() { Text = "RJ", Value = "RJ" });
            estados.Add(new SelectListItem() { Text = "RN", Value = "RN" });
            estados.Add(new SelectListItem() { Text = "RS", Value = "RS" });
            estados.Add(new SelectListItem() { Text = "RO", Value = "RO" });
            estados.Add(new SelectListItem() { Text = "RR", Value = "RR" });
            estados.Add(new SelectListItem() { Text = "SC", Value = "SC" });
            estados.Add(new SelectListItem() { Text = "SP", Value = "SP" });
            estados.Add(new SelectListItem() { Text = "SE", Value = "SE" });
            estados.Add(new SelectListItem() { Text = "TO", Value = "TO" });

            return estados;
        }
    }
}

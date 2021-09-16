using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Base;
using Publiparking.Web.Classes;
using Publiparking.Web.Configuration;
using Publiparking.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Controllers
{
    public class AnagraficaEnteController : UnloggedBaseController
    {
        private readonly DbParkContext _context;
        private readonly DatabaseConfig _databaseConfigSettings;
        [HttpPost]
        public JsonResult SelectEnte(int IdEnte)
        {
            ViewBag.showLoginBar = false;
            ViewBag.IsCDS = false;
            if (IdEnte > 0)
            {
                CambiaEnteWebViewModel selezionaEnteModel = new CambiaEnteWebViewModel();
                var v_ente = AnagraficaEnteBD.GetById(dbContextGeneraleReadOnly, IdEnte);   //dbContextGenerale.anagrafica_ente.Find(IdEnte);
                string valueAccountCookie = UnLogHttpContextAccessor.HttpContext.Request.Cookies["id_ente"];// Request.Cookies.Get(SPID_COOKIE) ?? new HttpCookie(SPID_COOKIE);
                HelperCookies.SetCookie("id_ente", IdEnte.ToString(), UnLogHttpContextAccessor, 20);
                //HttpContext.Session.SetComplexData("CurrentEnte", v_ente);
                //HttpContext.Session.SetString("IsLogged", "1");
                HelperCookies.SetCookie("IsLogged", "1", UnLogHttpContextAccessor, 20);
                selezionaEnteModel.selEnteId = IdEnte;
                string v_result = v_ente.url_ente + ";" + v_ente.descrizione_ente;
                return Json(v_result);
            }
            else
                return null;
        }
        public async Task<IActionResult> Index()
        {
            CambiaEnteWebViewModel modelEnte = new CambiaEnteWebViewModel();
            modelEnte.Descrizione_Ente = string.Empty;
            anagrafica_ente enteDefault = new anagrafica_ente
            {
                descrizione_ente = "Seleziona...",
                id_ente = 0,
            };
            if (modelEnte.listEnte == null)
            {
                modelEnte.listEnte = await dbContextGenerale.anagrafica_ente.ToListAsync();

            }

            return View("Index", modelEnte);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anagrafica_ente = await _context.anagrafica_ente
                .FirstOrDefaultAsync(m => m.id_ente == id);
            if (anagrafica_ente == null)
            {
                return NotFound();
            }

            return View(anagrafica_ente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_ente,codice_ente,descrizione_ente,cod_regione,cod_provincia,cod_comune,cap,indirizzo,tel1,tel2,id_tipo_ente,id_stato_contatto,flag_on_off,cod_fiscale,p_iva,data_ultimo_controllo_validita,nome_db,flag_Tipo_rendicontazione,flag_sportello,flag_presenze,flag_idrico,flag_sosta,user_name_db,password_db,indirizzo_ip_db,cod_ente,codice_concessione,tp_entity_id,email,pec,flag_tipo_gestione_pagopa,codice_ente_pagopa,aux_digit_pagopa,application_code_pagopa,codice_segregazione_pagopa,codice_struttura_ente_pagopa,CBILL,url_ente,id_risorsa_web")] anagrafica_ente anagrafica_ente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anagrafica_ente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anagrafica_ente);
        }
        // GET: anagrafica_ente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anagrafica_ente = await _context.anagrafica_ente.FindAsync(id);
            if (anagrafica_ente == null)
            {
                return NotFound();
            }
            return View(anagrafica_ente);
        }

        // POST: anagrafica_ente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_ente,codice_ente,descrizione_ente,cod_regione,cod_provincia,cod_comune,cap,indirizzo,tel1,tel2,id_tipo_ente,id_stato_contatto,flag_on_off,cod_fiscale,p_iva,data_ultimo_controllo_validita,nome_db,flag_Tipo_rendicontazione,flag_sportello,flag_presenze,flag_idrico,flag_sosta,user_name_db,password_db,indirizzo_ip_db,cod_ente,codice_concessione,tp_entity_id,email,pec,flag_tipo_gestione_pagopa,codice_ente_pagopa,aux_digit_pagopa,application_code_pagopa,codice_segregazione_pagopa,codice_struttura_ente_pagopa,CBILL,url_ente,id_risorsa_web")] anagrafica_ente anagrafica_ente)
        {
            if (id != anagrafica_ente.id_ente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anagrafica_ente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!anagrafica_enteExists(anagrafica_ente.id_ente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(anagrafica_ente);
        }

        // GET: anagrafica_ente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anagrafica_ente = await _context.anagrafica_ente
                .FirstOrDefaultAsync(m => m.id_ente == id);
            if (anagrafica_ente == null)
            {
                return NotFound();
            }

            return View(anagrafica_ente);
        }

        // POST: anagrafica_ente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anagrafica_ente = await _context.anagrafica_ente.FindAsync(id);
            _context.anagrafica_ente.Remove(anagrafica_ente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool anagrafica_enteExists(int id)
        {
            return _context.anagrafica_ente.Any(e => e.id_ente == id);
        }
    }
}

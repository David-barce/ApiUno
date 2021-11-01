using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiUno.Models;

namespace ApiUno.Controllers
{
    public class UsuariosController : ApiController
    {
        private DesarrolloMovilEntities db = new DesarrolloMovilEntities();

        // GET: api/Usuarios
        public IQueryable<DbUsuario> GetDbUsuario()
        {
            return db.DbUsuario;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(DbUsuario))]
        public IHttpActionResult GetDbUsuario(int id)
        {
            DbUsuario dbUsuario = db.DbUsuario.Find(id);
            if (dbUsuario == null)
            {
                return NotFound();
            }

            return Ok(dbUsuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDbUsuario(int id, DbUsuario dbUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dbUsuario.id)
            {
                return BadRequest();
            }

            db.Entry(dbUsuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbUsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(DbUsuario))]
        public IHttpActionResult PostDbUsuario(DbUsuario dbUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DbUsuario.Add(dbUsuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DbUsuarioExists(dbUsuario.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dbUsuario.id }, dbUsuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(DbUsuario))]
        public IHttpActionResult DeleteDbUsuario(int id)
        {
            DbUsuario dbUsuario = db.DbUsuario.Find(id);
            if (dbUsuario == null)
            {
                return NotFound();
            }

            db.DbUsuario.Remove(dbUsuario);
            db.SaveChanges();

            return Ok(dbUsuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DbUsuarioExists(int id)
        {
            return db.DbUsuario.Count(e => e.id == id) > 0;
        }
    }
}
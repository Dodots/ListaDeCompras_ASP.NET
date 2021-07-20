using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListaDeCompras.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ListaDeCompras.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        [AllowAnonymous]
        [Route("lista-itens")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            return View(await db.Itens.ToListAsync());

            
        }

        // GET: Items/Details/5
        [Route("detalhe-item/{id:int}")]
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Itens.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        [Route("novo-item")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public static byte[] ConverToByte(HttpPostedFileBase file)
        {
            var length = file.InputStream.Length; //Length: 103050706
            byte[] fileData;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }
            return fileData;
        }

        // GET: Items/Edit/5
        [Route("editar-item/{id:int}")]
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Itens.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Delete/5
        [Route("deletar-item/{id:int}")]
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Itens.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Create
        [Route("novo-item")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Descricao,Preco,Categoria,DataDeCompra,Imagem")] Item item)
        {
            
            if (ModelState.IsValid)
            {

                int arquivosSalvos = 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase arquivo = Request.Files[i];

                    //Suas validações ......

                    //Salva o arquivo
                    if (arquivo.ContentLength > 0)
                    {
                        byte[] fileData = ConverToByte(arquivo);
                        item.ImagemDB = fileData;
                        arquivosSalvos++;
                    }
                }

                db.Itens.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("");
        }

        // POST: Items/Edit/5
        [Route("editar-item/{id:int}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Descricao,Preco,Categoria,DataDeCompra,Imagem, ImagemDB")] Item item)
        {
            if (ModelState.IsValid)
            {
                int arquivosSalvos = 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase arquivo = Request.Files[i];

                    //Suas validações ......

                    //Salva o arquivo
                    if (arquivo.ContentLength > 0)
                    {
                        byte[] fileData = ConverToByte(arquivo);
                        item.ImagemDB = fileData;
                        arquivosSalvos++;
                    }
                }

                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [Route("deletar-item/{id:int}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Itens.FindAsync(id);
            db.Itens.Remove(item);
            await db.SaveChangesAsync();
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

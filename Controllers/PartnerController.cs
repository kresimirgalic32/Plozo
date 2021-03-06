using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OZO.Models;
using Microsoft.Extensions.Options;
using OZO.ViewModels;
using OZO.Extensions;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OZO.Controllers
{
  public class PartnerController : Controller
  {
    private readonly PI09Context ctx;
    private readonly AppSettings appData;

    public PartnerController(PI09Context ctx, IOptionsSnapshot<AppSettings> options)
    {
      this.ctx = ctx;
      appData = options.Value;
    }

    public IActionResult Index(string filter, int page = 1, int sort = 1, bool ascending = true)
    {
      int pagesize = appData.PageSize;      
      var query = ctx.VwPartner.AsQueryable();


      int count = query.Count();

      var pagingInfo = new PagingInfo
      {
        CurrentPage = page,
        Sort = sort,
        Ascending = ascending,
        ItemsPerPage = pagesize,
        TotalItems = count
      };
      if (page < 1)
      {
        page = 1;
      }
      else if (page > pagingInfo.TotalPages)
      {
        return RedirectToAction(nameof(Index), new { page = pagingInfo.TotalPages, sort = sort, ascending = ascending });
      }

      System.Linq.Expressions.Expression<Func<ViewPartner, object>> orderSelector = null;
      switch (sort)
      {
        case 1:
          orderSelector = p => p.IdPartnera;
          break;
        case 2:
          orderSelector = p => p.TipPartnera;
          break;
        case 3:
          orderSelector = p => p.Mbr;
          break;
        case 4:
          orderSelector = p => p.Naziv;
          break;
      }
      if (orderSelector != null)
      {
        query = ascending ?
               query.OrderBy(orderSelector) :
               query.OrderByDescending(orderSelector);
      }

      var partneri = query
                  .Skip((page - 1) * pagesize)
                  .Take(pagesize)
                  .ToList();
      var model = new PartneriViewModel
      {
        Partneri = partneri,
        PagingInfo = pagingInfo
      };

      return View(model);
    }



    [HttpGet]
    public IActionResult Create()
    {
      PartnerViewModel model = new PartnerViewModel
      {
        TipPartnera = "O"
      };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PartnerViewModel model)
    {      
      if (ModelState.IsValid)
      {
        Partner p = new Partner();
        p.TipPartnera = model.TipPartnera;
        CopyValues(p, model);
        try
        {
          ctx.Add(p);
          ctx.SaveChanges();

          TempData[Constants.Message] = $"Partner uspje??no dodan. Id={p.IdPartnera}";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Index));

        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return View(model);
        }
      }
      else
      {
        return View(model);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int IdPartnera, int page = 1, int sort = 1, bool ascending = true)
    {
      var partner = await ctx.Partner.FindAsync(IdPartnera);
      if (partner != null)
      {
        try
        {
          ctx.Remove(partner);
          await ctx.SaveChangesAsync();
          TempData[Constants.Message] = $"Partner {partner.IdPartnera} uspje??no obrisan.";
          TempData[Constants.ErrorOccurred] = false;
        }
        catch (Exception exc)
        {
          TempData[Constants.Message] = "Pogre??ka prilikom brisanja partnera: " + exc.CompleteExceptionMessage();
          TempData[Constants.ErrorOccurred] = true;
        }
      }
      else
      {
        TempData[Constants.Message] = "Ne postoji partner s id-om: " + IdPartnera;
        TempData[Constants.ErrorOccurred] = true;
      }
      return RedirectToAction(nameof(Index), new { page = page, sort = sort, ascending = ascending });
    }

    [HttpGet]
    public IActionResult Edit(int id, int page = 1, int sort = 1, bool ascending = true)
    {
      var partner = ctx.Partner.Find(id);
      if (partner == null)
      {
        return NotFound("Ne postoji partner s oznakom: " + id);
      }
      else
      {
        PartnerViewModel model = new PartnerViewModel
        {
          IdPartnera = partner.IdPartnera,
          AdrIsporuke = partner.AdrIsporuke,
          AdrPartnera = partner.AdrPartnera,
          Mbr = partner.Mbr,
          TipPartnera = partner.TipPartnera
        };
        if (model.TipPartnera == "O")
        {
          Osoba osoba = ctx.Osoba.AsNoTracking()
                           .Where(o => o.IdOsobe == model.IdPartnera)
                           .First(); //Single()
          model.ImeOsobe = osoba.ImeOsobe;
          model.PrezimeOsobe = osoba.PrezimeOsobe;
        }
        else
        {
          Tvrtka tvrtka = ctx.Tvrtka.AsNoTracking()
                           .Where(t => t.IdTvrtke == model.IdPartnera)
                           .First(); //Single()
          model.MbrTvrtke = tvrtka.MbrTvrtke;
          model.NazivTvrtke = tvrtka.NazivTvrtke;
        }



        ViewBag.Page = page;
        ViewBag.Sort = sort;
        ViewBag.Ascending = ascending;
        return View(model);
      }
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PartnerViewModel model, int page = 1, int sort = 1, bool ascending = true)
    {
      if (model == null)
      {
        return NotFound("Nema poslanih podataka");
      }
      var partner = ctx.Partner.Find(model.IdPartnera);
      if (partner == null)
      {
        return NotFound("Ne postoji partner s id-om: " + model.IdPartnera);
      }      

      if (ModelState.IsValid)
      {
        try
        {
          CopyValues(partner, model);

          //vezani dio je stvoren s new Osoba() ili new Tvrtka() pa je entity stated Added ??to bi proizvelo Insert pa ne update
          if (partner.Osoba != null)
          {
            partner.Osoba.IdOsobe = partner.IdPartnera;
            ctx.Entry(partner.Osoba).State = EntityState.Modified;
          }
          if (partner.IdTvrtkeNavigation!= null)
          {
            partner.IdTvrtkeNavigation.IdTvrtke = partner.IdPartnera;
            ctx.Entry(partner.IdTvrtkeNavigation).State = EntityState.Modified;
          }

          ctx.SaveChanges();
          TempData[Constants.Message] = $"Partner {model.IdPartnera} uspje??no a??uriran";
          TempData[Constants.ErrorOccurred] = false;
          return RedirectToAction(nameof(Index), new { page = page, sort = sort, ascending = ascending });

        }
        catch (Exception exc)
        {
          ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
          return View(model);
        }
      }
      else
      {
        return View(model);
      }
    }


    #region Private methods
    private void CopyValues(Partner partner, PartnerViewModel model)
    {
      partner.AdrIsporuke = model.AdrIsporuke;
      partner.AdrPartnera = model.AdrPartnera;
      partner.Mbr = model.Mbr;
      if (partner.TipPartnera == "O")
      {
        partner.Osoba = new Osoba();
        partner.Osoba.ImeOsobe = model.ImeOsobe;
        partner.Osoba.PrezimeOsobe = model.PrezimeOsobe;
      }
      else
      {
        partner.IdTvrtkeNavigation = new Tvrtka();
        partner.IdTvrtkeNavigation.MbrTvrtke = model.MbrTvrtke;
        partner.IdTvrtkeNavigation.NazivTvrtke = model.NazivTvrtke;
      }

    }

//     private void DohvatiNaziveMjesta(PartnerViewModel model)
//     {
//       try
//       {
//         //dohvati nazive mjesta                
//         if (model.IdMjestaPartnera.HasValue)
//         {
//           var mjesto = ctx.Mjesto
//                           .Where(m => m.IdMjesta == model.IdMjestaPartnera.Value)
//                           .Select(m => new { m.PostBrMjesta, m.NazMjesta })
//                           .First();
//           model.NazMjestaPartnera = string.Format("{0} {1}", mjesto.PostBrMjesta, mjesto.NazMjesta);
//         }
//         if (model.IdMjestaIsporuke.HasValue)
//         {
//           var mjesto = ctx.Mjesto
//                           .Where(m => m.IdMjesta == model.IdMjestaIsporuke.Value)
//                           .Select(m => new { m.PostBrMjesta, m.NazMjesta })
//                           .First();
//           model.NazMjestaIsporuke = string.Format("{0} {1}", mjesto.PostBrMjesta, mjesto.NazMjesta);
//         }
//       }
//       catch (Exception)
//       {
//         //TO DO Log error (npr. s NLogom)
//         throw;
//       }
//     }
    
     #endregion
//   }
} }
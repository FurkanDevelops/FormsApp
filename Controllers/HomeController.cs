using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
   

    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Products;

        if (!string.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString; // Arama ifadesini ViewBag'e ekle
            searchString = searchString.ToLower(); // Arama ifadesini de küçük harfe çevir
            products = products.Where(p => p.Name!.ToLower().Contains(searchString)).ToList();
        }
        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            ViewBag.Category = category; // Kategori seçimini ViewBag'e ekle
            products = products.Where(p => p.CategoryId.ToString() == category).ToList();
        }

        // ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name", category); // Kategorileri dropdown için ayarla
        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category // Seçili kategoriyi ayarla
        };
        
        return View(model);
    }
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View();
    }
    // Post edilince bu kısım çalışacak
    [HttpPost]
    public async Task<IActionResult> Create(Product model,IFormFile imageFile)
    {   
        var extension = Path.GetExtension(imageFile.FileName); // Yüklenen dosyanın uzantısını alıyoruz
        var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); // Rastgele bir dosya adı oluşturuyoruz
        // yüklenen resimi kaydetme işlemi 
        // wwwroot/img klasörüne kaydediyoruz
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", randomFileName);
        if(ModelState.IsValid)
        {
            using(var stream = new FileStream(path, FileMode.Create))
            {
                // Yüklenen dosyayı belirtilen dosya akışına kopyala
                // Bu işlem dosyanın sunucuya kaydedilmesini sağlar
                await imageFile.CopyToAsync(stream); 
            }
            model.ImageUrl =randomFileName; // Resim URL'sini ayarla
            model.ProductId = Repository.Products.Max(p => p.ProductId) + 1; // Yeni ürün için ID oluşturma
            // Model geçerli ise işlemleri yap
            // Örneğin, veritabanına kaydetme işlemi burada yapılabilir
            // Repository.CreateProduct(model); // Repository'de CreateProduct metodunu çağırabilirsiniz
            // RedirectToAction("Index"); // İşlem tamamlandıktan sonra yönlendirme yapabilirsiniz
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model); // Model geçerli değilse tekrar formu göster
        
    }

    public IActionResult Edit(int? id)
    {
        if(id==null)
        {
            return NotFound(); // ID null ise hata döndür
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id); // Ürünü bul
        if(entity == null)
        {
            return NotFound(); // Ürün bulunamazsa hata döndür
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(entity);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int? id,Product model, IFormFile imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound(); // ID null ise hata döndür
        }

        if(ModelState.IsValid)
        {
            
            if(imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName); // Yüklenen dosyanın uzantısını alıyoruz
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); // Rastgele bir dosya adı oluşturuyoruz
                // yüklenen resimi kaydetme işlemi 
                // wwwroot/img klasörüne kaydediyoruz
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", randomFileName);
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream); // Yüklenen dosyayı belirtilen dosya akışına kopyala
                }
                model.ImageUrl = randomFileName;
            }
            Repository.EditProduct(model); // Ürünü güncelle
            return RedirectToAction("Index"); // İşlem tamamlandıktan sonra yönlendirme yapabilirsiniz
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
    }
    public IActionResult Delete(int id)
    {
        if(id==null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id); // Ürünü bul
        if(entity == null)
        {
            return NotFound(); // Ürün bulunamazsa hata döndür
        }
        return View("Delete",entity);
    }
    [HttpPost]
    public IActionResult Delete(int? id,int ProductId )
    {
        if(id == null || id != ProductId)
        {
            return NotFound(); // ID null ise hata döndür
        }
        
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id); // Ürünü bul
        Repository.DeleteProduct(entity); // Ürünü sil
        return RedirectToAction("Index"); // İşlem tamamlandıktan sonra yönlendirme yapabilirsiniz
    }
}
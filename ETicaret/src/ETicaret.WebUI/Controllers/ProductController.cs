using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;
using ETicaret.Entities.Concrete;
using ETicaret.WebUI.Data;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IReviewService _reviewService;
    private readonly UserManager<AppUser> _userManager;

    public ProductController(IProductService productService, IReviewService reviewService, UserManager<AppUser> userManager)
    {
        _productService = productService;
        _reviewService = reviewService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Detail(int id)
    {
        var vm = await BuildAsync(id);
        return vm is null ? NotFound() : View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken, Authorize]
    public async Task<IActionResult> AddReview(int id, [Bind(Prefix = nameof(ProductDetailViewModel.NewReview))] ReviewSaveDto dto)
    {
        if (!ModelState.IsValid)
        {
            var vm = await BuildAsync(id);
            if (vm is null) return NotFound();
            vm.NewReview = dto;

            const string prefix = nameof(ProductDetailViewModel.NewReview) + ".";
            var errors = ModelState.Where(e => !e.Key.StartsWith(prefix) && e.Value is not null)
                                   .SelectMany(e => e.Value!.Errors.Select(err => (e.Key, err.ErrorMessage)))
                                   .ToList();
            foreach (var (key, message) in errors) ModelState.AddModelError(prefix + key, message);
            ViewData["OpenTab"] = "reviews";
            return View(nameof(Detail), vm);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        var result = await _reviewService.CreateAsync(id, user.Id, ReviewService.ShortName(user.FirstName, user.LastName), dto);
        TempData[result.Success ? "Success" : "Error"] = result.Message;
        return Redirect(Url.Action(nameof(Detail), new { id }) + "#reviews");
    }

    [HttpPost, ValidateAntiForgeryToken, Authorize]
    public async Task<IActionResult> DeleteReview(int id, int reviewId)
    {
        var result = await _reviewService.DeleteAsync(reviewId, _userManager.GetUserId(User)!, User.IsInRole(DbSeeder.AdminRole));
        TempData[result.Success ? "Success" : "Error"] = result.Message;
        return Redirect(Url.Action(nameof(Detail), new { id }) + "#reviews");
    }

    private async Task<ProductDetailViewModel?> BuildAsync(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null || !product.IsActive) return null;

        var userId = _userManager.GetUserId(User);
        return new ProductDetailViewModel
        {
            Product = product,
            Specs = ProductSpecs.Parse(product.Specifications),
            Installments = Installments.For(product.Price),
            Reviews = await _reviewService.GetSummaryAsync(id),
            Related = await _productService.GetRelatedAsync(id),
            CanReview = userId is not null && !await _reviewService.HasReviewedAsync(id, userId)
        };
    }
}

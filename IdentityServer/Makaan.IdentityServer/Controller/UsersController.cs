using Makaan.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Makaan.IdentityServer.Controller
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserSaveViewModel userSaveViewModel)
        {
            // UserName'deki boşlukları kaldır
            userSaveViewModel.UserName = userSaveViewModel.UserName?.Trim().Replace(" ", "");

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userSaveViewModel.UserName,
                Email = userSaveViewModel.Email,
                City = userSaveViewModel.City
            };
            var result = await _userManager.CreateAsync(applicationUser,userSaveViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            var roleAssignmentResult = await _userManager.AddToRoleAsync(applicationUser, "Member");

            if (!roleAssignmentResult.Succeeded)
            {
                return BadRequest(roleAssignmentResult.Errors.Select(x => x.Description));
            }

            return Ok("Kullanıcı Başarı İle Oluşturuldu");
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userClaim.Value);
                                    
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                City = user.City
                

            });

        }

        [HttpGet]
        public async Task<IActionResult> GetUserRole(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, Password);

            if (user == null || isPasswordValid == false)
            {
                return Ok("");
            }

            var userRole = await _userManager.GetRolesAsync(user);
            return Ok(userRole.FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetUserRoleByEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return Ok("Kullanici yok!");
            }

            var userRole = await _userManager.GetRolesAsync(user);
            return Ok(userRole.FirstOrDefault());
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel updateRoleViewModel)
        {
            var user = await _userManager.FindByEmailAsync(updateRoleViewModel.Email);

            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı !");
            }

            //Eski Rolu Sil
            var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, "Member");

            if (!removeRoleResult.Succeeded)
            {
                return BadRequest("Rol Silme Başarısız!");
            }

            //Yeni Rol Ekle
            var addRoleResult = await _userManager.AddToRoleAsync(user, "EstateAgent");

            if (!addRoleResult.Succeeded)
            {
                return BadRequest("Rol Ekleme Başarısız");
            }

            return Ok("Rol Başarı İle Güncellendi !");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı!");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            return Ok("Kullanıcı başarıyla silindi!");
        }//CreateEstateAgent

        [HttpPost]
        public async Task<IActionResult> CreateEstateAgent(UserSaveViewModel userSaveViewModel)
        {
            // UserName'deki boşlukları kaldır
            userSaveViewModel.UserName = userSaveViewModel.UserName?.Trim().Replace(" ", "");

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userSaveViewModel.UserName,
                Email = userSaveViewModel.Email,
                City = userSaveViewModel.City
            };
            var result = await _userManager.CreateAsync(applicationUser, userSaveViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            var roleAssignmentResult = await _userManager.AddToRoleAsync(applicationUser, "EstateAgent");

            if (!roleAssignmentResult.Succeeded)
            {
                return BadRequest(roleAssignmentResult.Errors.Select(x => x.Description));
            }

            return Ok("Kullanıcı Başarı İle Oluşturuldu");
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(user => new
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                City = user.City
            }).ToList();

            return Ok(users);
        }
        [HttpGet]
        public async Task<IActionResult> GetEstateAgentUsers()
        {
            // Tüm kullanıcıları veritabanından al
            var users = await _userManager.Users.ToListAsync();

            // EstateAgent rolüne sahip olan kullanıcıları filtrele
            var estateAgentUsers = new List<object>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "EstateAgent"))
                {
                    estateAgentUsers.Add(new
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        City = user.City
                    });
                }
            }

            return Ok(estateAgentUsers);
        }
        [HttpGet]
        public async Task<IActionResult> GetMemberUsers()
        {
            // Tüm kullanıcıları veritabanından al
            var users = await _userManager.Users.ToListAsync();

            // EstateAgent rolüne sahip olan kullanıcıları filtrele
            var meberUsers = new List<object>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Member"))
                {
                    meberUsers.Add(new
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,
                        City = user.City
                    });
                }
            }

            return Ok(meberUsers);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string email, string newPassword)
        {
            // Kullanıcıyı e-posta ile bul
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı!");
            }

            // Eski şifreyi kaldır
            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                return BadRequest(removePasswordResult.Errors.Select(x => x.Description));
            }

            // Yeni şifreyi ekle
            var addPasswordResult = await _userManager.AddPasswordAsync(user, newPassword);
            if (!addPasswordResult.Succeeded)
            {
                return BadRequest(addPasswordResult.Errors.Select(x => x.Description));
            }

            return Ok("Şifre başarıyla güncellendi!");
        }



    }
}

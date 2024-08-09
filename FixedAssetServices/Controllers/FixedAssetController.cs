using FixedAssetServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FixedAssetServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedAssetController : ControllerBase
    {
        private readonly FixedAssestContext db;
        private readonly IConfiguration _configuration;

        public FixedAssetController(IConfiguration configuration, FixedAssestContext _db)
        {
            _configuration = configuration;
            db = _db;
        }


        [HttpGet("FetchFixedAssetsById")]
        public ActionResult<FixedAssestResponse> FetchFixedAssetsById(string id)
        {
            var response = new FixedAssestResponse();
            try
            {
                var assetResponse = db.FixedAssetDetails
                .FromSqlRaw("EXEC proc_FetchFixedssetByID @id", new SqlParameter("@id", id))
                .AsEnumerable().FirstOrDefault();

                if (assetResponse == null)
                {
                    response.ResponseCode = "10";
                    response.ResponseDescription = "Fixed Asset details not found";

                    return NotFound(response);
                }

                response.ResponseCode = "00";
                response.ResponseDescription = "Fixed Asset details Retreived Successfuly";
                response.FixedAsset = assetResponse;

                return Ok(response);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.ResponseCode = "1000";
                response.ResponseDescription = message + "An error occurred";
                return StatusCode(500,response);
            }
            
        }


        [HttpGet("GetAllBranches")]
        public ActionResult<BranchResponse> GetAllBranches()
        {
            var response = new BranchResponse();
            try
            {
                var branches = db.Branch
                    .FromSqlRaw("SELECT BranchCode, BranchName FROM tbl_branch WHERE status = 1").AsEnumerable()
                    .ToList();

                

                if (branches == null || !branches.Any())
                {
                    response.ResponseCode = "10";
                    response.ResponseDescription = "No branches found";

                    return NotFound(response);
                }

                response.ResponseCode = "10";
                response.ResponseDescription = "Branches retrieved successfully";
                response.Branches = branches;

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }

        [HttpGet("GetAllUsers")]
        public ActionResult<UserResponse> GetAllUsers()
        {
            var response = new UserResponse();
            try
            {
                var users = db.Users
                    .FromSqlRaw("SELECT userid, fullname FROM tbl_userprofile WHERE status = 1").AsEnumerable()
                    .ToList();

                if (users == null || !users.Any())
                {
                    response.ResponseCode = "10";
                    response.ResponseDescription = "No User found";

                    return NotFound(response);
                }

                response.ResponseCode = "10";
                response.ResponseDescription = "Users retrieved successfully";
                response.Users = users;

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }


        [HttpGet("GetAllDepartments")]
        public ActionResult<DeptResponse> GetAllDepartments()
        {
            var response = new DeptResponse();
            try
            {
                var dep = db.Department
                    .FromSqlRaw("select deptid,DeptName from tbl_Department where status = 1").AsEnumerable()
                    .ToList();

                if (dep == null || !dep.Any())
                {
                    response.ResponseCode = "10";
                    response.ResponseDescription = "No Department found";

                    return NotFound(response);
                }

                response.ResponseCode = "10";
                response.ResponseDescription = "Department retrieved successfully";
                response.departments = dep;

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }

        [HttpGet("GetAllCategories")]
        public ActionResult<CategoryResponse> GetAllCategories()
        {
            var response = new CategoryResponse();
            try
            {
                var categories = db.Categories
                    .FromSqlRaw("SELECT categorycode, categoryname FROM tbl_fixedassetcategory WHERE status = 1")
                    .AsEnumerable()
                    .ToList();

                if (categories == null || !categories.Any())
                {
                    response.ResponseCode = "10";
                    response.ResponseDescription = "No categories found";

                    return NotFound(response);
                }

                response.ResponseCode = "00";
                response.ResponseDescription = "Categories retrieved successfully";
                response.Categories = categories;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }

        [HttpGet("FetchExpFixedAssetsByCat")]
        public ActionResult<FetchExpFixedAssetsByCatResponse> FetchExpFixedAssetsByCat(string categorycode)
        {
            var response = new FetchExpFixedAssetsByCatResponse();
            try
            {
                var fixedAssets = db.FixedAssetCat
                    .FromSqlRaw("EXEC proc_FetchExpFixedAssetsByCat @catid",
                        new SqlParameter("@catid", categorycode))
                    .AsEnumerable()
                    .ToList();

                if (fixedAssets == null || !fixedAssets.Any())
                {
                    response.ResponseCode = "10";
                    response.ResponseDescription = "No expired fixed assets found for the specified category";
                    return NotFound(response);
                }

                response.ResponseCode = "00";
                response.ResponseDescription = "Expired fixed assets retrieved successfully";
                response.FixedAssets = fixedAssets;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }


        [HttpPost("AllocateFAsset")]
        public ActionResult<AllocateFAssetResponse> AllocateFAsset([FromBody] AllocateFAssetRequest request)
        {
            var response = new AllocateFAssetResponse();
            try
            {
                var retvalParam = new SqlParameter
                {
                    ParameterName = "@retval",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                var retmsgParam = new SqlParameter
                {
                    ParameterName = "@retmsg",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 150,
                    Direction = System.Data.ParameterDirection.Output
                };

                db.Database.ExecuteSqlRaw(
                    "EXEC proc_AllocateFAsset @id, @Branchcode, @DeptID, @OfficerID, @DateAllocated, @UserID, @AuthID, @actionType, @retval OUTPUT, @retmsg OUTPUT",
                    new SqlParameter("@id", request.Id),
                    new SqlParameter("@Branchcode", request.Branchcode),
                    new SqlParameter("@DeptID", request.DeptID),
                    new SqlParameter("@OfficerID", request.OfficerID),
                    new SqlParameter("@DateAllocated", request.DateAllocated),
                    new SqlParameter("@UserID", request.UserID),
                    new SqlParameter("@AuthID", DBNull.Value),
                    new SqlParameter("@actionType", request.ActionType),
                    retvalParam,
                    retmsgParam
                );

                response.ResponseCode = retvalParam.Value.ToString();
                response.ResponseDescription = retmsgParam.Value.ToString();

                if (response.ResponseCode != "0")
                {
                    return BadRequest(response);
                }

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }

        [HttpPost("InsFAMaintenance")]
        public ActionResult<InsFAMaintenanceResponse> InsFAMaintenance([FromBody] InsFAMaintenanceRequest request)
        {
            var response = new InsFAMaintenanceResponse();
            try
            {
                var retvalParam = new SqlParameter
                {
                    ParameterName = "@retval",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                var retmsgParam = new SqlParameter
                {
                    ParameterName = "@retmsg",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 150,
                    Direction = System.Data.ParameterDirection.Output
                };

                db.Database.ExecuteSqlRaw(
                    "EXEC proc_InsFAMaintenance @id, @MaintAmount, @MaintDesc, @maintDate, @VendorID, @FundSourceGL, @UserID, @AuthID, @retval OUTPUT, @retmsg OUTPUT",
                    new SqlParameter("@id", request.Id),
                    new SqlParameter("@MaintAmount", request.MaintAmount),
                    new SqlParameter("@MaintDesc", request.MaintDesc),
                    new SqlParameter("@maintDate", request.MaintDate),
                    new SqlParameter("@VendorID", DBNull.Value),
                    new SqlParameter("@FundSourceGL", request.FundSourceGL),
                    new SqlParameter("@UserID", request.UserID),
                    new SqlParameter("@AuthID", DBNull.Value),
                    retvalParam,
                    retmsgParam
                );

                response.ResponseCode = retvalParam.Value.ToString();
                response.ResponseDescription = retmsgParam.Value.ToString();

                if (response.ResponseCode != "0")
                {
                    return BadRequest(response);
                }

                return StatusCode(201,response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }

        [HttpPost("InsFARepairs")]
        public ActionResult<InsFARepairsResponse> InsFARepairs([FromBody] InsFARepairsRequest request)
        {
            var response = new InsFARepairsResponse();
            try
            {
                var retvalParam = new SqlParameter
                {
                    ParameterName = "@retval",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                var retmsgParam = new SqlParameter
                {
                    ParameterName = "@retmsg",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 150,
                    Direction = System.Data.ParameterDirection.Output
                };

                db.Database.ExecuteSqlRaw(
                    "EXEC proc_InsFARepairs @id, @RepairAmount, @RepairDesc, @RepairDate, @Capitalized, @AddedLifeSpan, @VendorID, @FundSourceGL, @UserID, @AuthID, @retval OUTPUT, @retmsg OUTPUT",
                    new SqlParameter("@id", request.Id),
                    new SqlParameter("@RepairAmount", request.RepairAmount),
                    new SqlParameter("@RepairDesc", request.RepairDesc),
                    new SqlParameter("@RepairDate", request.RepairDate),
                    new SqlParameter("@Capitalized", request.Capitalized),
                    new SqlParameter("@AddedLifeSpan", request.AddedLifeSpan),
                    new SqlParameter("@VendorID", DBNull.Value),
                    new SqlParameter("@FundSourceGL", request.FundSourceGL),
                    new SqlParameter("@UserID", request.UserID),
                    new SqlParameter("@AuthID", DBNull.Value),
                    retvalParam,
                    retmsgParam
                );

                response.ResponseCode = retvalParam.Value.ToString();
                response.ResponseDescription = retmsgParam.Value.ToString();

                if (response.ResponseCode != "0")
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }



        [HttpPost("DisposeFixedAsset")]
        public ActionResult<FixedAssetDisposalResponse> DisposeFixedAsset([FromBody] FixedAssetDisposalRequest request)
        {
            var response = new FixedAssetDisposalResponse();
            try
            {
                var retvalParam = new SqlParameter
                {
                    ParameterName = "@retval",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                var retmsgParam = new SqlParameter
                {
                    ParameterName = "@retmsg",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 150,
                    Direction = System.Data.ParameterDirection.Output
                };

                db.Database.ExecuteSqlRaw(
                    "EXEC proc_FixedAssetDisposalByID @id, @SalesAmount, @NetBookVal, @SalesGL, @SalesIncExpGL, @userid, @authid, @retval OUTPUT, @retmsg OUTPUT",
                    new SqlParameter("@id", request.Id),
                    new SqlParameter("@SalesAmount", request.SalesAmount),
                    new SqlParameter("@NetBookVal", request.NetBookVal),
                    new SqlParameter("@SalesGL", request.SalesGL),
                    new SqlParameter("@SalesIncExpGL", request.SalesIncExpGL),
                    new SqlParameter("@userid", request.UserId),
                    new SqlParameter("@authid", DBNull.Value),
                    retvalParam,
                    retmsgParam
                );

                response.ResponseCode = retvalParam.Value.ToString();
                response.ResponseDescription = retmsgParam.Value.ToString();

                if (response.ResponseCode != "0")
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }


        [HttpPost("DisposeFixedAssetList")]
        public ActionResult<FixedAssetDisposalListResponse> DisposeFixedAssetList([FromBody] FixedAssetDisposalListRequest request)
        {
            var response = new FixedAssetDisposalListResponse();
            try
            {
                var retvalParam = new SqlParameter
                {
                    ParameterName = "@retval",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                var retmsgParam = new SqlParameter
                {
                    ParameterName = "@retmsg",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 150,
                    Direction = System.Data.ParameterDirection.Output
                };

                var dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(string));
                dataTable.Columns.Add("SalesAmount", typeof(decimal));
                dataTable.Columns.Add("NetBookVal", typeof(decimal));
                dataTable.Columns.Add("SalesGL", typeof(string));
                dataTable.Columns.Add("SalesIncExpGL", typeof(string));

                foreach (var item in request.FixedAssetDisposalItems)
                {
                    dataTable.Rows.Add(item.Id, item.SalesAmount, item.NetBookVal, item.SalesGL, item.SalesIncExpGL);
                }

                var tvpParam = new SqlParameter
                {
                    ParameterName = "@Typ_FixedAssetDispList",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.Typ_FixedAssetDispList",
                    Value = dataTable
                };

                db.Database.ExecuteSqlRaw(
                    "EXEC proc_FixedAssetDisposalByList @Typ_FixedAssetDispList, @Trandate, @userid, @authid, @retval OUTPUT, @retmsg OUTPUT",
                    tvpParam,
                    new SqlParameter("@Trandate", request.TranDate),
                    new SqlParameter("@userid", request.UserId),
                    new SqlParameter("@authid", DBNull.Value),
                    retvalParam,
                    retmsgParam
                );

                response.ResponseCode = retvalParam.Value.ToString();
                response.ResponseDescription = retmsgParam.Value.ToString();

                if (response.ResponseCode != "0")
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "1000", ResponseDescription = ex.Message + " An error occurred" });
            }
        }

    }
}

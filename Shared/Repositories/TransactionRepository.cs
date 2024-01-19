using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Data.Context;
using Shared.Data.Contract;
using Shared.Data.Model;
using Shared.Interfaces;

namespace Shared.Repositories
{
    public class TransactionRepository : ITransactionRepositories
    {
        private readonly IConfiguration _configuration;
        private readonly ActuatorContext _context;
        public TransactionRepository(ActuatorContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<Tuple<bool, BaseResponse,BaseResponseValue<ResponseInsert>>> InsertDataBpkb(BaseRequest<InsertData> postData)
        {
            BaseResponseValue<ResponseInsert> responseValue = new BaseResponseValue<ResponseInsert>();
            ResponseInsert value = new ResponseInsert();
            BaseResponse result = new BaseResponse();
            int errorCode = 1002001;
            try
            {
                if (postData != null)
                {
                    if (postData.Value != null)
                    {
                        var cekDataUsers = _context.ms_user
                                                 .Where(x => x.user_id == postData.Value.user_id)
                                                 .FirstOrDefault() ?? new ms_user();
                        if (cekDataUsers.user_id > 0)
                        {
                            if (cekDataUsers.is_active)
                            {
                                tr_bpkb data = new tr_bpkb();
                                data.agreement_number = (!string.IsNullOrEmpty(postData.Value.agreement_number) ? postData.Value.agreement_number : "AGR-" + (Guid.NewGuid()).ToString());
                                data.bpkb_no = postData.Value.bpkb_no;
                                data.branch_id = postData.Value.branch_id;
                                data.faktur_no = postData.Value.faktur_no;
                                string[] formats = { "dd/mm/yyyy" };
                                data.faktur_date = DateTime.ParseExact(postData.Value.faktur_date, formats, new CultureInfo("id-ID"), DateTimeStyles.None);
                                data.location_id = postData.Value.location_id;
                                data.police_no = postData.Value.police_no;
                                data.bpkb_date_in = DateTime.ParseExact(postData.Value.bpkb_date_in, formats, new CultureInfo("id-ID"), DateTimeStyles.None);
                                data.bpkb_date = DateTime.ParseExact(postData.Value.bpkb_date, formats, new CultureInfo("id-ID"), DateTimeStyles.None);
                                data.created_by = cekDataUsers.user_name;
                                data.created_on = DateTime.Now;

                                await _context.AddAsync(data);
                                await _context.SaveChangesAsync();
                                if (!string.IsNullOrEmpty(data.agreement_number))
                                {
                                    value.agreement_Number = data.agreement_number;
                                    responseValue.Value = value;
                                    result = new ResponseError().result(0, false, "Berhasil disimpan");
                                    return new Tuple<bool, BaseResponse,BaseResponseValue<ResponseInsert>>(true, result,responseValue);
                                }
                                else
                                {
                                    result = new ResponseError().result(errorCode, true, "Gagal disimpan");
                                    return new Tuple<bool, BaseResponse, BaseResponseValue<ResponseInsert>>(false, result, responseValue);
                                }
                            }
                            else
                            {
                                throw new Exception("Data user tidak dapat melakukan transaksi ini");
                            }
                        }
                        else
                        {
                            throw new Exception("Data user tidak ditemukan");
                        }
                    }
                    else
                    {
                        throw new Exception("Bad Request");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }
            catch (Exception e)
            {
                result = new ResponseError().result(errorCode, true, e.Message);
                return new Tuple<bool, BaseResponse, BaseResponseValue<ResponseInsert>>(false, result, responseValue);
            }
        }
    }
}

	
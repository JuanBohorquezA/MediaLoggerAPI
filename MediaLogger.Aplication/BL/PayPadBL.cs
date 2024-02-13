using AutoMapper;
using MediaLogger.Application.Validation;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Persistence.SQLServer;
using Microsoft.Extensions.Configuration;

namespace MediaLogger.Application.BL
{
    public class PayPadBL  
    { 
    
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly PayPadRepository _paypadRepository;

        public PayPadBL(IMapper mapper, IConfiguration configuration, PayPadRepository payPadRepository)
        {
            _mapper = mapper;
            _configuration = configuration;
            _paypadRepository = payPadRepository;
        }
        public async Task<PayPadDto?> GetByIdAsync(int id)
        {
            var paypad =  _mapper.Map<PayPadDto>(await _paypadRepository.GetByIdAsync(id));
            return paypad;
        }


        public async Task<List<PayPadDto>?> GetAllAsync()
        {
            var paypads =  _mapper.Map<List<PayPadDto>>(await _paypadRepository.GetAllAsync());
            return paypads;

        }

        public async Task<PayPadDto?> GetByUsernameAsync(string? username)
        {
            var paypad = _mapper.Map<PayPadDto>((await _paypadRepository.GetAllAsync()).Where(p => p.USERNAME == username).FirstOrDefault());
            
            return paypad;
        }

        public async Task<string?> GetPaypadPasswordAsync(string username)
        {
            var paypad = await this.GetByUsernameAsync(username);
            if (paypad == null) throw new Exception("No se encontró paypad");

            return await _paypadRepository.GetPaypadPasswordAsync(Convert.ToInt32(paypad.Id));
        }


    }
}

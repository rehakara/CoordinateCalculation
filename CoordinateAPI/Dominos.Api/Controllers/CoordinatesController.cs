using AutoMapper;
using Dominos.Api.DTO;
using Dominos.Core.Models;
using Dominos.Core.Services;
using Dominos.Messaging.Send.Sender;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Dominos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinatesController : ControllerBase
    {
        private readonly ICoordinateService coordinateService;
        private readonly IMapper mapper;
        private readonly ICoordinateSender coordinateSender;
        private readonly IFileService fileService;

        public CoordinatesController(ICoordinateService coordinateService, IMapper mapper, ICoordinateSender coordinateSender, IFileService fileService)
        {
            this.coordinateService = coordinateService;
            this.mapper = mapper;
            this.coordinateSender = coordinateSender;
            this.fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coordinate>>> GetAllCoordinates()
        {
            var coordinates = await coordinateService.GetAllCoordinates();

            var coordinateResources = mapper.Map<IEnumerable<Coordinate>, IEnumerable<CoordinateDTO>>(coordinates);
            return Ok(coordinateResources); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Coordinate>>> GetCoordinateById(long id)
        {
            var coordinate = await coordinateService.GetCoordinateById(id);

            var coordinateResource = mapper.Map<Coordinate, CoordinateDTO>(coordinate);
            return Ok(coordinateResource);
        }

        [HttpPost]
        public async Task<ActionResult> SendCoordinates()
        {
            var coordinates = await coordinateService.GetAllCoordinates();
            var processStartTime = DateTime.Now;
            var watch = Stopwatch.StartNew();
            watch.Start();

            foreach (var coordinate in coordinates)
            {
                coordinateSender.SendCoordinate(coordinate);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var processEndTime = DateTime.Now;
            fileService.WriteToOutput(processStartTime, processEndTime, elapsedMs);

            return Ok();
        }
    }
}

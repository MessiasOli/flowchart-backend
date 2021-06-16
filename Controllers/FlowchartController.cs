using Agricultural_Plan.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using Agricultural_Plan.Service;

namespace Agricultural_Plan.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowchartController : Controller
    {
        private readonly IRepository<FlowchartJson> flowchartJsonRepository;
        private readonly IRepository<MatirialInput> matirialInputRepository;
        private readonly IRepository<Area> areaRepository;

        public FlowchartController(NODEContext context)
        {
            this.flowchartJsonRepository = new Repository<FlowchartJson>(context);
            this.matirialInputRepository = new Repository<MatirialInput>(context);
            this.areaRepository = new Repository<Area>(context);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var nodes = flowchartJsonRepository.GetAll();

            return Ok(JsonConvert.SerializeObject(nodes));
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var node = flowchartJsonRepository.GetById(id);

                if (node == null)
                {
                    return NotFound();
                }

                return Ok(JsonConvert.SerializeObject(node));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Flowchart id: {id}! Erro: {e}");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] FlowchartJson flow)
        {
            try
            {
                flow.lastEdition = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                var hasFlow = flowchartJsonRepository.GetById(flow.id);

                if (hasFlow != null)
                {
                    flowchartJsonRepository.Delete(hasFlow);
                    flowchartJsonRepository.Add(flow);
                    return Get(flow.id);
                }

                flowchartJsonRepository.Add(flow);

                return Get(flow.id);
            }
            catch (Exception e)
            {
                return StatusCode( StatusCodes.Status409Conflict, $"Node id: {flow.id} já existente! Erro: {e}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutOrAdd(int id, [FromBody] FlowchartJson flowIn)
        {
            try
            {
                var node = flowchartJsonRepository.GetById(id);

                if (node == null)
                {
                    flowchartJsonRepository.Add(flowIn);
                    return Get(flowIn.id);
                }

                flowchartJsonRepository.Delete(node);
                flowchartJsonRepository.Add(flowIn);

                return Get(flowIn.id);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Node id: {flowIn.id}! Erro: {e}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var node = flowchartJsonRepository.GetById(id);

                if (node == null)
                {
                    return NotFound();
                }

                flowchartJsonRepository.Delete(node);

                return Ok($"Nó id: {id} apagado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Node id: {id} Falha ao excluir! Erro: {e}");
            }
        }

        [HttpPost("addareas")]
        public ActionResult AddAreas([FromBody] List<Area> area)
        {
            try
            {
                area.ForEach(a =>
                {
                    var hasMaterial = areaRepository.GetById(a.id);

                    if (hasMaterial != null)
                    {
                        hasMaterial.CopyFrom(a);
                        areaRepository.Update(hasMaterial);
                    }
                    else
                    {
                        areaRepository.Add(a);
                    }

                });

                return Ok("Areas inseridas com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Falha ao salvar as Areas!");
            }
        }

        [HttpGet("getareas")]
        public ActionResult GetArea()
        {
            try
            {
                var node = areaRepository.GetAll();

                if (node == null)
                {
                    return NotFound();
                }

                return Ok(JsonConvert.SerializeObject(node));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"GetArea! Erro: {e}");
            }
        }

        [HttpPost("material")]
        public ActionResult AddMatirialInput([FromBody] MatirialInput material)
        {
            try
            {
                var hasMaterial = matirialInputRepository.GetById(material.idarea, material.idmaterial);

                if (hasMaterial != null)
                {
                    matirialInputRepository.Delete(hasMaterial);
                    matirialInputRepository.Add(material);
                    return GetMatirialInput(material.idarea, material.idmaterial);
                }

                matirialInputRepository.Add(material);

                return GetMatirialInput(material.idarea, material.idmaterial);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Node id: {material.idarea} já existente! Erro: {e}");
            }
        }

        [HttpPut("material")]
        public ActionResult UpdateMatirialInput([FromBody] MatirialInput material)
        {
            try
            {
                var hasMaterial = matirialInputRepository.GetById(material.idarea, material.idmaterial);

                if (hasMaterial != null)
                {
                    matirialInputRepository.Delete(hasMaterial);
                    matirialInputRepository.Add(material);
                    return GetMatirialInput(material.idarea, material.idmaterial);
                }

                matirialInputRepository.Add(material);

                return GetMatirialInput(material.idarea, material.idmaterial);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Node id: {material.idarea} já existente!");
            }
        }

        [HttpDelete("material/{idarea}/{idmaterial}")]
        public ActionResult DeleteMatirialInput(string idarea, string idmaterial)
        {
            try
            {
                var hasMaterial = matirialInputRepository.GetById(idarea, idmaterial);

                if (hasMaterial != null)
                {
                    matirialInputRepository.Delete(hasMaterial);
                    return Ok("Material apagado com sucesso!");
                }

                return Ok("Falha ao apagar o item");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Node id: {idarea} já existente! Erro: {e}");
            }
        }

        [HttpPut("getmaterialinput/{idarea}")]
        public ActionResult GetMatirialInput(string idarea, string idmaterial)
        {
            try
            {
                var node = matirialInputRepository.GetById(idarea, idmaterial);

                if (node == null)
                {
                    return NotFound();
                }

                return Ok(JsonConvert.SerializeObject(node));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"GetMaterial id: {idarea}! Erro: {e}");
            }
        }

        [HttpGet("getmaterials/{idarea}")]
        public ActionResult GetMaterials(string idArea)
        {
            try
            {
                List<MatirialInput> materials = (from material in matirialInputRepository.GetByQuery()
                                                 where material.idarea == idArea 
                                                 orderby material.description
                                                 select material).ToList<MatirialInput>();

                return Ok(JsonConvert.SerializeObject(materials));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ops... algum problema com a base de dados.");
            }
        }

        [HttpGet("getreportview/{idarea}")]
        public ActionResult GetReportView(string idArea)
        {
            try
            {
                Area area = areaRepository.GetById(idArea);

                List<MatirialInput> materials = (from material in matirialInputRepository.GetByQuery()
                                                 where material.idarea == idArea
                                                 select material).ToList<MatirialInput>();

                GenerateReportView generator = new GenerateReportView(area);

                materials.ForEach(mat =>
                {
                    generator.AddMaterial(mat);
                });

                return Ok(JsonConvert.SerializeObject(generator.GetReport()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ops... algum problema com ao gerar o relatório.");
            }
        }
    }
}

using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using RestLayer.Model;
using System;

namespace RestLayer.Controllers
{
    [Route("api/Klant")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IManager manager;

        public ClientController(IManager manager)
        {
            this.manager = manager;
        }


        #region Clients
        /// <summary>
        /// POST a client to the database
        /// </summary>
        /// <param name="client">CLient to POST.</param>
        /// <returns>THe cliented added to the database.</returns>
        [HttpPost]
        public ActionResult<RClientOutput> PostClient([FromBody] RClientInput client)
        {
            try
            {
                if (client == null)
                {
                    throw new RestException("Er moet een client zijn om te posten");
                }

                int id = manager.AddClient(Mapper.ToClient(client));
                return CreatedAtAction(nameof(GetClient), new { id }, Mapper.ToRClientOutput(manager.GetClient(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// GET a client from the database
        /// </summary>
        /// <param name="id">Id of the client to GET</param>
        /// <returns>The client from the database.</returns>
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<RClientOutput> GetClient(int id)
        {
            try
            {
                return Ok(Mapper.ToRClientOutput(manager.GetClient(id)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// PUT an updated client in the database.
        /// </summary>
        /// <param name="id">Id of the client.</param>
        /// <param name="client">Updted client.</param>
        /// <returns>The updated client.</returns>
        [HttpPut("{id}")]
        public ActionResult<RClientOutput> PutClient(int id, [FromBody] RClientInput client)
        {
            try
            {
                if (client == null)
                {
                    throw new RestException("Er moet een client zijn om te putten");
                }

                if (id != client.Id)
                {
                    throw new RestException("De id in de url en in de body komen niet overeen.");
                }
                manager.UpdateClient(id, Mapper.ToClient(client));
                return Ok(Mapper.ToRClientOutput(manager.GetClient(id)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// DELETE a client from the database.
        /// </summary>
        /// <param name="id">Id of the client.</param>
        /// <returns>A NOCOntent response.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            try
            {
                manager.DeleteClient(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Het gegeven klantId is niet in de database")
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Orders
        [HttpPost("{id}/Bestelling")]
        public ActionResult<ROrderOutput> PostOrder(int id, [FromBody] ROrderInput orderInfo)
        {
            try
            {

                if (orderInfo == null)
                {
                    throw new RestException("Er moet een order zijn om te posten.");
                }
                if (id != orderInfo.ClientId)
                {
                    throw new RestException("De id in de url en in de body komen niet overeen.");
                }

                int newOrderId = manager.MakeOrder(orderInfo.ClientId, Mapper.ToProductType(orderInfo.Product), orderInfo.Amount);

                return Ok(Mapper.ToROrderOutput(manager.GetOrder(newOrderId, id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}/Bestelling/{orderId}")]
        [HttpHead("{id}/Bestelling/{orderId}")]
        public ActionResult<ROrderOutput> GetOrder(int id, int orderId)
        {
            try
            {
                return Ok(Mapper.ToROrderOutput(manager.GetOrder(orderId, id)));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Bestelling/{orderId}")]
        public ActionResult<ROrderOutput> PutOrder(int id, int orderId, [FromBody] ROrderInput orderInfo)
        {
            try
            {
                if (orderInfo == null)
                {
                    throw new RestException("Er moet een order zijn om te posten.");
                }
                if (id != orderInfo.ClientId)
                {
                    throw new RestException("De id van de klant in de url en in de body komen niet overeen.");
                }
                if (orderId != orderInfo.OrderId)
                {
                    throw new RestException("De id van de bestelling in de url en in de body komen niet overeen.");
                }
                manager.UpdateOrder(id, orderId, Mapper.ToProductType(orderInfo.Product), orderInfo.Amount);
                return Ok(Mapper.ToROrderOutput(manager.GetOrder(orderId, id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/Bestelling/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                manager.DeleteOrder(orderId);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Het gegeven orderId is niet in de database")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using CommonModels;

namespace CarrierServices
{
    public class DefaultCommonLogicProvider : ICommonLogicProvider
    {
        public IEnumerable<ItemTrackingDetail> ProcessShipDetails(List<ShipDetail> shipDetails, string carrierName)
        {
            foreach (ShipDetail shipDetail in shipDetails)
            {
                yield return new ItemTrackingDetail
                {
                    CarrierName = carrierName,
                    ShippingId = shipDetail.ShipId,
                    Items =  shipDetail.Items
                        .Select(item => new ItemTrackingInfo
                            {
                                Sku = item.Sku,
                                TrackingNumber = GenerateTrackingNumber()
                            })
                        .ToList()
                };
            }
        }

        private static string GenerateTrackingNumber()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}

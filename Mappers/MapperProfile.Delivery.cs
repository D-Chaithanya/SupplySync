using AutoMapper;
using SupplySync.DTOs.Delivery;
using SupplySync.Models;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;

namespace SupplySync.Mappers
{
    public partial class MapperProfile
    {
        private void ConfigureDeliveryMappings()
        {
            /*
             * ===============================
             * CreateDeliveryRequestDto → Delivery
             * ===============================
             */
            CreateMap<CreateDeliveryRequestDto, Delivery>()
                // Flatten first item name if Item not explicitly passed
                .ForMember(
                    dest => dest.Item,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrWhiteSpace(src.Item)
                            ? src.Item
                            : src.Items != null && src.Items.Count > 0
                                ? src.Items[0].Item
                                : null
                    )
                )
                // Sum quantities if not provided directly
                .ForMember(
                    dest => dest.Quantity,
                    opt => opt.MapFrom(src =>
                        src.Quantity > 0
                            ? src.Quantity
                            : src.Items != null
                                ? src.Items.Sum(i => i.Quantity)
                                : 0
                    )
                )
                // Serialize items list into JSON column
                .ForMember(
                    dest => dest.ItemsJson,
                    opt => opt.MapFrom(src =>
                        src.Items != null && src.Items.Count > 0
                            ? JsonSerializer.Serialize(src.Items)
                            : null
                    )
                )
                // Managed by backend
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());


            /*
             * ===============================
             * Delivery → DeliveryResponseDto
             * ===============================
             */
            CreateMap<Delivery, DeliveryResponseDto>()
                // Deserialize items JSON into DeliveryItemDto list
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(src =>
                        !string.IsNullOrEmpty(src.ItemsJson)
                            ? JsonSerializer.Deserialize<List<DeliveryItemDto>>(src.ItemsJson)!
                            : new List<DeliveryItemDto>()
                    )
                );
        }
    }
}
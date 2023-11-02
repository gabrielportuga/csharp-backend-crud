using AutoMapper;
using KanbanBoard.Api.Domain.Dtos;
using KanbanBoard.Api.Domain.Models;

public class KanbanBoardProfile : Profile
{
    public KanbanBoardProfile()
    {
        CreateMap<Card, CardDto>();
        CreateMap<CardDto, Card>();
    }
}

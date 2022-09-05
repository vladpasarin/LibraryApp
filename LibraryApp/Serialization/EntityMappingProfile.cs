using AutoMapper;
using LibraryApp.DTOs;
using LibraryApp.DTOs.Assets;
using LibraryApp.Entities;
using LibraryApp.Entities.Assets.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Serialization
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Asset, AssetDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<AudioBook, AudioBookDto>().ReverseMap();
            CreateMap<EBook, EBookDto>().ReverseMap();
            CreateMap<AvailabilityStatus, AvailabilityStatusDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Badge, BadgeDto>().ReverseMap();
            CreateMap<Challenge, ChallengeDto>().ReverseMap();
            CreateMap<Checkout, CheckoutDto>().ReverseMap();
            CreateMap<CheckoutHistory, CheckoutHistoryDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Discussion, DiscussionDto>().ReverseMap();
            CreateMap<Hold, HoldDto>().ReverseMap();
            CreateMap<LibraryCard, LibraryCardDto>().ReverseMap();
            CreateMap<ReadingLog, ReadingLogDto>().ReverseMap();
            CreateMap<ReplyTo, ReplyToDto>().ReverseMap();
            CreateMap<UserDto, User>();
            CreateMap<BookmarkDto, Bookmark>().ReverseMap();
            CreateMap<Book, GenericBookDto>().ReverseMap();
            CreateMap<EBook, GenericBookDto>().ReverseMap();
            CreateMap<AudioBook, GenericBookDto>().ReverseMap();
            CreateMap<TagDto, Tag>().ReverseMap();
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<UserChallenge, UserChallengeDto>().ReverseMap();
            CreateMap<Goal, GoalDto>().ReverseMap();
            CreateMap<GoalType, GoalTypeDto>().ReverseMap();
            CreateMap<Quote, QuoteDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}

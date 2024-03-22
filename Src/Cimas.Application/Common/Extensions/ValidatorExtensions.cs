using FluentValidation;

namespace Cimas.Application.Common.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, IEnumerable<TElement>> MustHaveUniqueIds<T, TElement, TId>(
            this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder, Func<TElement, TId> idSelector)
            where TId : notnull
        {
            return ruleBuilder.Must(list => list.Select(idSelector).Distinct().Count() == list.Count())
                .WithMessage("All Ids must be unique");
        }

        public static IRuleBuilderOptions<T, IEnumerable<TElement>> MustBeValidEnum<T, TElement, TEnum>(
            this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder, Func<TElement, TEnum> enumSelector)
            where TEnum : struct, Enum
        {
            IEnumerable<TEnum> enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            IEnumerable<string> enumNamesWithValues = enumValues.Select(e => $"{Convert.ToInt32(e)} - {Enum.GetName(typeof(TEnum), e)}");
            string enumDescriptions = string.Join(", ", enumNamesWithValues);

            return ruleBuilder.Must(list => list.All(item => Enum.IsDefined(typeof(TEnum), enumSelector(item))))
                .WithMessage($"All enum values must be valid. Valid values are: {enumDescriptions}");
        }
    }
}

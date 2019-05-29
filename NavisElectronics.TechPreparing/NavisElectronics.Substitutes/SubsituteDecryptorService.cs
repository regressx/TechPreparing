namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Сервис расшифровки допустимых замен
    /// </summary>
    public class SubsituteDecryptorService
    {
        /// <summary>
        /// The _actual subsitutes decriptor.
        /// </summary>
        private readonly ActualSubsitutesDecryptor _actualSubsitutesDecriptor;

        /// <summary>
        /// The _sub groups decriptor.
        /// </summary>
        private readonly ISubGroupsDecryptor _subGroupsDecriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubsituteDecryptorService"/> class.
        /// </summary>
        /// <param name="actualSubsitutesDecriptor">
        /// The actual Subsitutes Decriptor.
        /// </param>
        /// <param name="subGroupsDecriptor">
        /// The sub groups decriptor.
        /// </param>
        public SubsituteDecryptorService(ActualSubsitutesDecryptor actualSubsitutesDecriptor, SubGroupsDecryptor subGroupsDecriptor)
        {
            _actualSubsitutesDecriptor = actualSubsitutesDecriptor;
            _subGroupsDecriptor = subGroupsDecriptor;
        }

        /// <summary>
        /// Метод умеет расшифровывать группу заменителей
        /// </summary>
        /// <param name="group">
        /// The group.
        /// </param>
        public void DecriptSub(SubstituteGroup group)
        {
            // расшифровываем актуальный заменитель
            _actualSubsitutesDecriptor.DecriptActualSubstitutes(group, false);

            // эта строка пойдет потом в дополнение к элементам группы допустимых заменителей
            string forSubs =
                _actualSubsitutesDecriptor.GetInfoAboutSubsititutionsForSub(group, false);

            // расшифровываем группы допустимых заменителей
            _subGroupsDecriptor.DescriptSubGroups(group.SubGroups);

            // эта строка пойдет в дополнение к актуальному заменителю
            string forActual = _subGroupsDecriptor.GetInfoAboutGroupsForActualElement(group.SubGroups);

            foreach (var actual in group.ActualSub)
            {
                actual.SubstituteInfo = actual.SubstituteInfo + " " + forActual;
            }
            for (int i = 0; i < group.SubGroups.Count; i++)
            {
                foreach (var subs in group.SubGroups[i].Subsitutes)
                {
                    subs.SubstituteInfo = subs.SubstituteInfo + " " + forSubs;
                }
            }

            // этим методом добавляем к группам допустимых заменителей элементы через союз "или"
            _subGroupsDecriptor.SetInfoAboutSubstitutesFromAnotherGroupsRecursive(group.SubGroups);
        }

    }
}
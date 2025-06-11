using DocumentManagement.Domain.StepNames;

namespace DocumentManagement.Domain.Steps;

internal class Step : BaseEntity
{
    public StepName? StepName { get; set; }  
}

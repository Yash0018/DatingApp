import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

// Using the CaDeactivateGuard to make check when deactivating a component
// Passing in MemberEditComponent to tell componenet what it wants to deactivate
export const preventUnsavedChangesGuard: CanDeactivateFn<
  MemberEditComponent
> = (component) => {
  if (component.editForm?.dirty) {
    return confirm(
      'Are you sure you want to continue? Any unsaved changes will be lost'
    );
  }

  return true;
};

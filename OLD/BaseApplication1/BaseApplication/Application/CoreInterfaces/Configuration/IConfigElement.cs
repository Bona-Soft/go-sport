using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration
{
	public interface IConfigElement
	{
		//
		// Summary:
		//     Gets an System.Configuration.ElementInformation object that contains the non-customizable
		//     information and functionality of the System.Configuration.ConfigurationElement
		//     object.
		//
		// Returns:
		//     An System.Configuration.ElementInformation that contains the non-customizable
		//     information and functionality of the System.Configuration.ConfigurationElement.
		ElementInformation ElementInformation { get; }

		//
		// Summary:
		//     Gets the collection of locked attributes.
		//
		// Returns:
		//     The System.Configuration.ConfigurationLockCollection of locked attributes (properties)
		//     for the element.
		ConfigurationLockCollection LockAllAttributesExcept { get; }

		//
		// Summary:
		//     Gets the collection of locked elements.
		//
		// Returns:
		//     The System.Configuration.ConfigurationLockCollection of locked elements.
		ConfigurationLockCollection LockAllElementsExcept { get; }

		//
		// Summary:
		//     Gets the collection of locked attributes
		//
		// Returns:
		//     The System.Configuration.ConfigurationLockCollection of locked attributes (properties)
		//     for the element.
		ConfigurationLockCollection LockAttributes { get; }

		//
		// Summary:
		//     Gets the collection of locked elements.
		//
		// Returns:
		//     The System.Configuration.ConfigurationLockCollection of locked elements.
		ConfigurationLockCollection LockElements { get; }

		//
		// Summary:
		//     Gets or sets a value indicating whether the element is locked.
		//
		// Returns:
		//     true if the element is locked; otherwise, false. The default is false.
		//
		// Exceptions:
		//   T:System.Configuration.ConfigurationErrorsException:
		//     The element has already been locked at a higher configuration level.
		bool LockItem { get; set; }
	}
}
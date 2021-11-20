﻿using commonItems;
using Xunit;

namespace ImperatorToCK3.UnitTests.Imperator.Families {
	[Collection("Sequential")]
	[CollectionDefinition("Sequential", DisableParallelization = true)]
	public class FamiliesTests {
		[Fact]
		public void FamiliesDefaultToEmpty() {
			var reader = new BufferedReader(
				"=\n" +
				"{\n" +
				"}"
			);
			var families = new ImperatorToCK3.Imperator.Families.Families();
			families.LoadFamilies(reader);

			Assert.Empty(families);
		}

		[Fact]
		public void FamiliesCanBeLoaded() {
			var reader = new BufferedReader(
				"=\n" +
				"{\n" +
				"42={}\n" +
				"43={}\n" +
				"}"
			);
			var families = new ImperatorToCK3.Imperator.Families.Families();
			families.LoadFamilies(reader);

			Assert.Collection(families,
				item => {
					Assert.Equal((ulong)42, item.Key);
					Assert.Equal((ulong)42, item.Value.Id);
				},
				item => {
					Assert.Equal((ulong)43, item.Key);
					Assert.Equal((ulong)43, item.Value.Id);
				}
			);
		}

		[Fact]
		public void LiteralNoneFamiliesAreNotLoaded() {
			var reader = new BufferedReader(
				"={\n" +
				"42=none\n" +
				"43={}\n" +
				"44=none\n" +
				"}"
			);
			var families = new ImperatorToCK3.Imperator.Families.Families();
			families.LoadFamilies(reader);

			Assert.Collection(families,
				item => {
					Assert.Equal((ulong)43, item.Key);
					Assert.Equal((ulong)43, item.Value.Id);
				}
			);
		}
	}
}
